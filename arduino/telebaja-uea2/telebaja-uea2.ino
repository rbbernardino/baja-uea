//Este código lê os valores dos sensores acoplados ao Baja Uea, a saber sensores de temperatura do motor,
//velocidade do móvel, nível de combustível no tanque, acionamento do freio e rotações por minuto do motor. 


#include "LiquidCrystal.h"
#include "Limits.h"
#include "SoftwareSerial.h"


//Freio________________________________
const int leitorTensao0 = 12;           //Pino analógico que o resistor pull up está conectado.
float valorLeitorTensao0 = 0;          //Freio=="0". Ou seja, toda variável que tem "0" está relacionada ao acionamento do freio
float Voltagem0 = 0;
char estado[] = "ind";                   //ind é a indicação do freio
char Ativado = 'L';                         //variavel de teste

//Temperatura_________________________
const int leitorTensao2 = 11;          //Pino analógico que o resistor NTC está conectado.
float valorLeitorTensao2 = 0;         //Temperatura=="2". Ou seja, toda variável que tem "2" está relacionada a temperatura.
float VR2 = 0, R2 = 0;
int Temp = 0;


//Rpm________________________________
const int leitorTensao3 = 9;         //Pino analógico que o Conversor FT está conectado.
float valorLeitorTensao3 = 0;        //Rpm=="3". Ou seja, toda variável que tem "3" está relacionada a RPM
float VR3 = 0;
int rpm3 = 0;


//Velocidade__________________________
const int leitorTensao4 = 8;         //Pino analógico que o Conversor FT está conectado.
float valorLeitorTensao4 = 0;        //Velocidade=="4". Ou seja, toda variável que tem "4" está relacionada a velocidade
float VR4 = 0;
int vel4 = 0;


//Criando um objeto da classe LiquidCrystal e 
//inicializando com os pinos da interface.
LiquidCrystal lcd(33, 35, 45, 47, 49, 51);

// XBee__________________________

// ---------- PROTOCOLO --------------
// os valores dessas const devem estar exatamente igual aos da aplicação C#
const char SND_CONNECT = 'C'; // ENVIA 'C' para PC, pedindo para se conectar
const char RCV_OK = 'K'; // RECEBE do PC indiciando que pode começar o envio

const char SND_BEGIN = 'B'; // vai começar envio de dados dos sensores (1 pacote)
const char SND_END = 'E'; // encerrou envio de dados dos sensores (1 pacote)

const String SND_OK = "OK\r";
const String RCV_READY = "READY"; // PC pergunta se arduino está pronto
const String RCV_START = "START"; // PC pede que inicie envio de medicoes

// ---------- CONSTANTES -------------
// endereço de destino (SH/SL do XBee do PC)
String PC_ID = "XBEE_PC";
const int XBEE_DELAY = 10; // 10ms de delay entre o envio de cada byte para o XBee
const int XBEE_CMD_DELAY = 10; // parâmetro AT GT do XBee do carro
const int CHECK_CONN_INTERVAL = 1000; // verifica sinal do PC a cada 1s

 // flag para indicar se deve ou não checar qualidade do sinal DURANTE a gravação
const bool CHECK_PC_SIGNAL = true;

// ---------- VARIÁVEIS --------------
SoftwareSerial XBSerial(52, 53); // RX (shield: 2), TX (shield: 3)
unsigned long current_millis; // para calcular o tempo e sincronizar pacotes
unsigned long previous_millis; // para calcular timeouts de timers
byte current_millisByte[4];

bool connected_to_pc;
bool test_mode; // indica se manda msg p/ PC ou apenas mostra leitura dos sensores

String rssi_str;
int rssi_db; // byte do rssi em decibes (-28~-98 dB)
//unsigned long prev_millis;
//const int RSSI_RFRSH_INTERVAL = 2000;

void setup()
{
	//Inicializando o LCD e informando o tamanho de 16 colunas e 2 linhas
	//que é o tamanho do LCD JHD 1602byy usado neste projeto.
	lcd.begin(16, 2);
	lcd.print("Iniciando...");

	// espera 2 segundos para o XBee dar boot e o usuário ter um feedback visual
	delay(2000);

	// configura XBee e conecta com o PC
	XBSerial.begin(9600);

	if(connectToPC())
	{
		test_mode = false;

		print_lcd_full("Conectado!",
			"Forca:" + rssi_to_perc(rssi_db) + " " + String(rssi_db) + "dB");
		delay(3500);

		print_lcd_full("Aguardando", "inicio...");

		waitStart();

		print_lcd_full("Pronto!", "iniciando... ");
		delay(1500);
		lcd.clear();
	}
	else
	{
		print_lcd_full("Sinal nao encon-", "trado!");
		delay(1500);
		print_lcd_full("Iniciando no    ", "modo teste...");
		delay(1500);

		test_mode = true;
	}
}

bool connectToPC()
{
	print_lcd_full("Verificando sinal", "do carro...");
	if (ATrcv_DN(PC_ID)) // verifica nó "XBEE_CARRO" na rede e seta DH, DI (endereço de destino)
	{
		ATrcv_DB(); // verifica forca do sinal
		return true;
	}
	else
		return false;
}

bool waitStart()
{
	String received_msg = xbee_read_line(); // aguarda prox msg
	
	// quando o waitStart() retorna, volta ao setup() que por sua vez, vai para o loop()
	if (received_msg.equals(RCV_READY))
	{
		XBSerial.write("OK\r");
		received_msg = xbee_read_line();  // aguarda prox msg

		if (received_msg.equals(RCV_START))
			return true;
		else
		{
			throw_error(3);
			return false;
		}
	}
	else
	{
		throw_error(3);
		return false;
	}
}

// recebe mensagem de forma sincrona
char receive_sync()
{
	while (XBSerial.available() < 1)
	{ /* mantém no loop até receber dados */
	}

	// ao sair do while terá dado para ler
	return XBSerial.read();
}

// funções para troca de mensagens com comando AT
bool ATrcv_ok()
{
	while (XBSerial.available() < 3)
	{ }

	String msg = String();
	msg += (char)XBSerial.read();
	msg += (char)XBSerial.read();
	msg += (char)XBSerial.read();
	return (msg == "OK\r");
}

// envia comando ATDN para verificar se está conectado com o PC
// seta flag "connected_to_pc"
// se os endereços de destino não estiveram setados, esse comando automaticamente seta
bool ATrcv_DN(String id)
{
	String rcv_msg = String();

	// entrar no command mode
	ATcmd();

	// verifica conexao
	XBSerial.print("ATDN" + id + "\r");
	rcv_msg = xbee_read_line();

	if (rcv_msg.equals("OK"))
	{
		connected_to_pc = true;
		return true;
	}
	else if (rcv_msg.equals("ERROR"))
	{
		connected_to_pc = false;
		return false;
	}
	else
	{
		throw_error(4);
		return -1; // nunca executa esse "return", está aqui apenas para compilar
	}
	// ATDN: sai automaticamente do command mode após resposta
}

// atualiza variáveis do rssi do último pacote
void ATrcv_DB()
{
	// entrar no command mode
	ATcmd();
	
	// solicita info do RSSI do último pacote
	XBSerial.print("ATDB\r");
	while (XBSerial.available() < 3);
	rssi_str = xbee_read_line();

	// sair do command mode
	XBSerial.print("ATCN\r");
	if (!ATrcv_ok())
		throw_error(1);

	char temp_buf[2];
	temp_buf[0] = rssi_str.charAt(0);
	temp_buf[1] = rssi_str.charAt(1);

	rssi_db = -1 * strtol(temp_buf, 0, 16);
}

void ATcmd() {
	delay(XBEE_CMD_DELAY * 3);
	XBSerial.print("+++");

	delay(100);
	if (!ATrcv_ok())
		throw_error(1);
}

String xbee_read_line()
{
	String line = String();
	char temp_char;
	while (true)
	{
		// espera até ter o que ler
		while (!XBSerial.available());

		// verifica se é final de linha, se for vai para a proxima linha
		temp_char = XBSerial.read();
		if ((int) temp_char == '\r')
			break;
		else
			line += temp_char;
	}
	return line;
}

void xbee_ignore_lines(int number_of_lines)
{
	for (int i = 0; i < number_of_lines; i++)
	{
		while (true)
		{
			// espera até ter o que ler
			while (!XBSerial.available());

			// verifica se é final de linha, se for vai para a proxima linha
			if (XBSerial.read() == '\r')
				break;
		}

		// a versão abaixo é para DEBUG, imprime as linhas sendo ignoradas...
		//lcd.clear();
		//lcd.print("ln" + String(i));
		//lcd.setCursor(0, 1);
		//while (true)
		//{
		//	// espera até ter o que ler
		//	while (!XBSerial.available());
		//	char c = XBSerial.read();
		//	// verifica se é final de linha, se for vai para a proxima linha
		//	if (c == '\r')
		//		break;
		//	else
		//		lcd.print(c);
		//}
		//delay(1500);
	}
}

void throw_error(int error_id)
{
	String ln1 = "!! ERRO " + String(error_id) + " !!";
	String ln2 = String();

	switch (error_id)
	{
		// erro ao tentar entrar no modo command, OK não foi retornado
		case 1:
			ln2 = "rcv AT OK fail";
			break;
		// ao conetar com outro XBee configurado com ID que não bate com PC_ID
		case 2:
			ln2 = "cnct XBee descnh";
			break;
		// caso o cliente esteja implementando o protocolo em DESACORDO com o arduino
		case 3:
			ln2 = "err de protocolo";
			break;
		// caso a resposta de um comando AT dê em algo inesperado
		case 4:
			ln2 = "err resp cmd AT";
			break;
		default:
			ln2 = "erro descnh...";
			break;
	}
	while(true);
}

void debug_print(String ln1) { debug_print(ln1, ""); }
void debug_print(String ln1, String ln2)
{
	print_lcd_full(ln1, ln2); while (true);
}

void print_lcd_full(String line1) { print_lcd_full(line1, ""); }
void print_lcd_full(String line1, String line2)
{
	lcd.clear();
	lcd.print(line1);
	lcd.setCursor(0, 1);
	lcd.print(line2);
}

//	RSSI:
//		db >= -50 db = 100% quality
//		db <= -100 db = 0 % quality
//  Between -50db and -100db:
//      quality ~= 2 * (db + 100)
//      RSSI ~= (percentage / 2) - 100
//	For example :
//		High quality : 90 % ~= -55db
//		Medium quality : 50 % ~= -75db
//		Low quality : 30 % ~= -85db
//		Unusable quality : 8 % ~= -96db
String rssi_to_perc(int rssi)
{
	if (rssi <= -96) return "8%";
	else if (rssi < -85) return "10%";
	else if (rssi <= -85) return "30%";
	else if (rssi <= -75) return "50%";
	else if (rssi < -50) return "90%";
	else if (rssi >= -50) return "100%";
	else return "..."; // nunca ocorre
}

// 3 chars no max
String rssi_to_quality(int rssi)
{
	if (rssi <= -96) return "!Er";
	else if (rssi < -85) return "!Lo";
	else if (rssi <= -85) return "Low";
	else if (rssi <= -75) return "Med";
	else if (rssi < -50) return "Hi";
	else if (rssi >= -50) return "Hi+";
	else return "..."; // nunca ocorre
}

void loop()
{
	ler_dados();
	print_lcd();
	EnviaXBee();

	// verifica se ainda está conectado ao PC e atualiza qualidade do sinal
	if (CHECK_PC_SIGNAL)
	{
		// o current_millis é obtido no EnviaXBee, que é enviado ao PC
		if (current_millis - previous_millis >= CHECK_CONN_INTERVAL)
		{
			if (ATrcv_DN(PC_ID))
				ATrcv_DB();
			previous_millis = current_millis;
		}
	}
	else
		delay(100);
	
}

// Lê dados dos sensores e armazena as medições em variáveis
void ler_dados() {
	//__________________________________________________________________________________  
	//Acionamento do Freio
	//Leitor de tensão para freio
	valorLeitorTensao0 = analogRead(leitorTensao0);
	Voltagem0 = 5 * valorLeitorTensao0 / 1023; //1023 é o valor máximo digital para 5V
	
	if (Voltagem0 >= 3)           //nível de segurança entre detecção de low e high level
		Ativado = 'H';
	else
		Ativado = 'L';


	//__________________________________________________________________________________  
	//Nível de Combustível
	//Leitor de tensão para Nivel
	//valorLeitorTensao1 = analogRead(leitorTensao1);
	//VR1 = 5 * valorLeitorTensao1 / 1023; //1023 é o valor máximo digital para 5V
	//R1 = ((5 * 2190) / (5 - VR1)) - 2190;    //VALOR EM OHMS =2190 (valor do resistor 2k2 aproximadamente.
	//Nivel = (-100 * (R1 - Rmin)) / (Rmin - Rmax);
	//Vnivel = Nivel;
	//if (Nivel<0)
	//{
	//	Vnivel = 0; //devido a variações do próprio sensor... Para casos de erro na medição por vibração do carro, pista irregular, etc...
	//}


	//__________________________________________________________________________________  
	//Temperatura do Motor   
	//Leitor de tensão para temperatura
	valorLeitorTensao2 = analogRead(leitorTensao2);
	VR2 = 5 * valorLeitorTensao2 / 1023; //1023 é o valor máximo digital para 5V
	R2 = ((5 * 5622) / (5 - VR2)) - 5622;    //VALOR EM OHMS =5622 (valor do resistor 5k6 aproximadamente.
	if (R2 >= 4200)
	{
		Temp = 50;    //para evitar mostrar valores baixos de temperatura, nos quais o sistema não irá atuar, são mostradas as temperaturas acima de 50ºC 
	}
	else
	{
		//valores da curva ajustados para o Arduino por meio de interpolação
		Temp = (((-1.403*0.000000000000001)*pow(R2, 5))) + ((1.75*(0.00000000001))*pow(R2, 4)) - ((8.404*(0.00000001))*pow(R2, 3)) + ((1.998*(0.0001))*pow(R2, 2)) - ((0.2614)*(R2)) + 238.6;
	}

	//__________________________________________________________________________________  
	//Rotação do motor 
	//Leitor de tensão
	valorLeitorTensao3 = analogRead(leitorTensao3);
	VR3 = 5 * valorLeitorTensao3 / 1023; //1023 é o valor máximo digital para 5V
	if (VR3>0.01)
	{
		rpm3 = ((3000 * VR3)) / 3.04;//equação que converte tensão em frequencia
	}
	else
	{
		rpm3 = 0;
	}



	//__________________________________________________________________________________  
	//Velocidade 
	//Leitor de tensão
	valorLeitorTensao4 = analogRead(leitorTensao4);
	VR4 = 5 * valorLeitorTensao4 / 1023; //1023 é o valor máximo digital para 5V
	if (VR4>0.1)
	{
		vel4 = ((3.6 * 2 * 3.1416*VR4)*(0.4));//equação que converte tensão em frequencia
											  //OBS: a última constante da fórmula acima é o raio da roda do Baja, em metros (1m/s=3.6km/h).
	}
	else
	{
		vel4 = 0;
	}
}

// imprime valores lidos no lcd
void print_lcd() {
	//Exibindo valor das variaveis monitoradas no display.
	lcd.clear();            //limpa o display do LCD.
	lcd.print("F:");        //impressao do estado do acionamento (da chave) do freio       
	if (Ativado == 'H')		 lcd.print("on ");
	else if (Ativado == 'L') lcd.print("off");

	//impressao do valor de temperatura (3 char)
	lcd.print("T:" + String(Temp));
	if (Temp < 100)
		 lcd.print(" "); // min 50, 2 caracteres + 1 espaço

	if (CHECK_PC_SIGNAL)
	{
		// impressao da forca do sinal - max 5 chars
		if (test_mode)
			lcd.print("<off>"); // quando nao conectado ao carro no inicio (setup)
		else
		{
			if (connected_to_pc)
			{
				lcd.print("S:");
				lcd.print(rssi_to_quality(rssi_db)); // rssi do último pacote (3chars)
													 //lcd.print(rssi_db);
			}
			else
				lcd.print("S:err"); // indica que não está conectado
		}
	}

	lcd.setCursor(0, 1);  //posiciona o cursor na coluna 0 linha 1 do LCD.

	//impressao do valor de rotação do motor (RPM) - max 4 chars
	lcd.print("Rpm:" + String(rpm3));
	if(rpm3 < 10)		 lcd.print("   ");
	else if(rpm3 < 100)  lcd.print("  ");
	else if(rpm3 < 1000) lcd.print(" ");
	
	lcd.print(" ");

	//impressao do valor da velocidade (km/h), sempre menor que 100
	lcd.print("Vel:");
	lcd.print(vel4);
}

// Envia valores lidos pelo XBee para o PC
// 80ms de delay são produzidos aqui
void EnviaXBee()
{
	//-------------------------------------------------------------------
	// Envia dados para o XBee/PC

	// flag (B)egin para cliente saber que iniciou o envio de um pacote de dados
	XBSerial.print(String(SND_BEGIN) + "\r");
	delay(XBEE_DELAY);

	// MILLIS
	current_millis = millis();
	writeLong(XBSerial, current_millis);
	delay(XBEE_DELAY);

	// FREIO
	//Ativado = 'L'; // TODO: remover, apenas teste!
	XBSerial.print(Ativado); // envia para XBee o character
	delay(XBEE_DELAY);

	// TEMPERATURA
	//Temp = 250; // TODO: remover, apenas teste!
	writeInt16(XBSerial, Temp);

	// RPM
	//rpm3 = 2000; // TODO: remover, apenas teste!
	writeInt16(XBSerial, rpm3);

	//vel4 = 50; // TODO: remover, apenas teste!
	writeInt8(XBSerial, vel4);

	// indica final do pacote
	XBSerial.print(String(SND_END) + "\r");
}

// envia um int pequeno (1 bytes) pela porta serial especificada
void writeInt8(SoftwareSerial pSerial, int num)
{
	pSerial.write(lowByte(num)); // envia 1 byte (menos significativo apenas)
	delay(XBEE_DELAY);
}

// envia um int completo (2 bytes) pela porta serial especificada
void writeInt16(SoftwareSerial pSerial, int num)
{
	pSerial.write(lowByte(num)); // envia 1 byte (menos significativo apenas)
	delay(XBEE_DELAY);
	pSerial.write(highByte(num)); // envia 1 byte (mais significativo apenas)
	delay(XBEE_DELAY);
}

void writeLong(SoftwareSerial pSerial, unsigned long num)
{
	LongToBytes(current_millis, current_millisByte);
	XBSerial.write((char*)current_millisByte, 4);
}

void LongToBytes(long val, byte b[4])
{
	b[3] = (byte)((val >> 24) & 0xff);
	b[2] = (byte)((val >> 16) & 0xff);
	b[1] = (byte)((val >> 8) & 0xff);
	b[0] = (byte)((val)& 0xff);
}