//Este c�digo l� os valores dos sensores acoplados ao Baja Uea, a saber sensores de temperatura do motor,
//velocidade do m�vel, n�vel de combust�vel no tanque, acionamento do freio e rota��es por minuto do motor. 


#include "LiquidCrystal.h"
#include "Limits.h"
#include "SoftwareSerial.h"


//Freio________________________________
const int leitorTensao0 = 8;           //Pino anal�gico que o resistor pull up est� conectado.
float valorLeitorTensao0 = 0;          //Freio=="0". Ou seja, toda vari�vel que tem "0" est� relacionada ao acionamento do freio
float Voltagem0 = 0;
char estado[] = "ind";                   //ind � a indica��o do freio
char Ativado = 'L';                         //variavel de teste

//Temperatura_________________________
const int leitorTensao2 = 10;          //Pino anal�gico que o resistor NTC est� conectado.
float valorLeitorTensao2 = 0;         //Temperatura=="2". Ou seja, toda vari�vel que tem "2" est� relacionada a temperatura.
float VR2 = 0, R2 = 0;
int Temp = 0;


//Rpm________________________________
const int leitorTensao3 = 11;         //Pino anal�gico que o Conversor FT est� conectado.
float valorLeitorTensao3 = 0;        //Rpm=="3". Ou seja, toda vari�vel que tem "3" est� relacionada a RPM
float VR3 = 0;
int rpm3 = 0;


//Velocidade__________________________
const int leitorTensao4 = 12;         //Pino anal�gico que o Conversor FT est� conectado.
float valorLeitorTensao4 = 0;        //Velocidade=="4". Ou seja, toda vari�vel que tem "4" est� relacionada a velocidade
float VR4 = 0;
int vel4 = 0;


//Criando um objeto da classe LiquidCrystal e 
//inicializando com os pinos da interface.
LiquidCrystal lcd(39, 41, 43, 45, 47, 49);

//Comunica��o XBee__________________________
// os valores dessas const devem estar exatamente igual aos da aplica��o C#
const char SND_CONNECT = 'C'; // ENVIA 'C' para PC, pedindo para se conectar
const char RCV_OK = 'K'; // RECEBE do PC indiciando que pode come�ar o envio
const char SND_READY = 'R'; // ENVIA 'R' para PC, avisando que est� pronto para enviar
const char RCV_START = 'S'; // RECEBE 'S' para iniciar envio de dados (loop)

const char SND_BEGIN = 'B'; // vai come�ar envio de dados dos sensores (1 pacote)

const int CONN_LED = 22; // led que indica se est� conectado com o pc

SoftwareSerial XBSerial(52, 53); // RX (shield: 2), TX (shield: 3)
const int XBEE_DELAY = 10; // 10ms de delay entre o envio de cada byte

unsigned long current_millis;
byte current_millisByte[4];

void setup()
{
	//Inicializando o LCD e informando o tamanho de 16 colunas e 2 linhas
	//que � o tamanho do LCD JHD 1602byy usado neste projeto.
	lcd.begin(16, 2);

	//pinMode(CONN_LED, OUTPUT);
	//digitalWrite(CONN_LED, LOW);

	//XBSerial.begin(9600);
	//connectToPC();
	//waitStart();
}

void connectToPC()
{
	XBSerial.print(SND_CONNECT);

	// re-envia CONNECT at� receber algo (handshake)
	while (XBSerial.available() < 1)
	{
		XBSerial.print(SND_CONNECT);
		delay(1500); // taxa de re-envio de 1,5s
	}

	// espera pelo OK do PC para concluir conexao (hanshake)
	char received_msg = receive_sync();
	while (received_msg != RCV_OK)
	{
		received_msg = receive_sync();
	}

	// ao sair do while envia sinal de que est� pronto, esperando START para entrar no loop()
	XBSerial.print(SND_READY);

	// LED indica que est� conectado
	digitalWrite(CONN_LED, HIGH);
}

void waitStart()
{
	char received_msg = receive_sync();
	while (received_msg != RCV_START)
	{
		received_msg = receive_sync();
	}

	// se sair do while significa que recebeu START, logo inicia loop de envio de dados
	// para tal apenas encerra o waitStart(), que volta ao setup() e retornar�

}

// recebe mensagem de forma sincrona
char receive_sync()
{
	while (XBSerial.available() < 1)
	{ /* mant�m no loop at� receber dados */
	}

	// ao sair do while ter� dado para ler
	return XBSerial.read();
}

void loop()
{
	ler_dados();
	print_lcd();
	//EnviaXBee(); // produz 80ms de delay
	//delay(70); // 70 + 80 = 150ms
	delay(150);
}

// L� dados dos sensores e armazena as medi��es em vari�veis
void ler_dados() {
	//__________________________________________________________________________________  
	//Acionamento do Freio
	//Leitor de tens�o para freio
	valorLeitorTensao0 = analogRead(leitorTensao0);
	Voltagem0 = 5 * valorLeitorTensao0 / 1023; //1023 � o valor m�ximo digital para 5V
	if (Voltagem0 >= 3)           //n�vel de seguran�a entre detec��o de low e high level
	{
		estado[1] = 'o';
		estado[2] = 'f';
		estado[3] = 'f';
		Ativado = 'L';
	}
	else
	{
		estado[1] = 'o';
		estado[2] = 'n';
		estado[3] = ' ';
		Ativado = 'H';
	}


	//__________________________________________________________________________________  
	//N�vel de Combust�vel
	//Leitor de tens�o para Nivel
	valorLeitorTensao1 = analogRead(leitorTensao1);
	VR1 = 5 * valorLeitorTensao1 / 1023; //1023 � o valor m�ximo digital para 5V
	R1 = ((5 * 2190) / (5 - VR1)) - 2190;    //VALOR EM OHMS =2190 (valor do resistor 2k2 aproximadamente.
	Nivel = (-100 * (R1 - Rmin)) / (Rmin - Rmax);
	Vnivel = Nivel;
	if (Nivel<0)
	{
		Vnivel = 0; //devido a varia��es do pr�prio sensor... Para casos de erro na medi��o por vibra��o do carro, pista irregular, etc...
	}



	//__________________________________________________________________________________  
	//Rota��o do motor 
	//Leitor de tens�o
	valorLeitorTensao3 = analogRead(leitorTensao3);
	VR3 = 5 * valorLeitorTensao3 / 1023; //1023 � o valor m�ximo digital para 5V
	if (VR3>0.01)
	{
		rpm3 = ((3000 * VR3)) / 1.04;//equa��o que converte tens�o em frequencia
	}
	else
	{
		rpm3 = 0;
	}



	//__________________________________________________________________________________  
	//Velocidade 
	//Leitor de tens�o
	valorLeitorTensao4 = analogRead(leitorTensao4);
	VR4 = 5 * valorLeitorTensao4 / 1023; //1023 � o valor m�ximo digital para 5V
	if (VR4>0.1)
	{
		vel4 = ((3.6 * 2 * 3.1416*VR4)*(0.4));//equa��o que converte tens�o em frequencia
											  //OBS: a �ltima constante da f�rmula acima � o raio da roda do Baja, em metros (1m/s=3.6km/h).
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
	lcd.print(estado[1]);
	lcd.print(estado[2]);
	//lcd.print(estado[3]); //comentado para exibir todas as variaveis no display ao mesmo tempo

	lcd.print("T:");     //impressao do valor de temperatura       
	lcd.print(Temp);


	lcd.setCursor(0, 1);  //posiciona o cursor na coluna 0 linha 1 do LCD.


	lcd.print("Rpm:");   //impressao do valor de rota��o do motor (RPM)       
	lcd.print(rpm3);
	lcd.print(" ");


	lcd.print("Vel:");   //impressao do valor da velocidade (km/h)       
	lcd.print(vel4);

}

// Envia valores lidos pelo XBee para o PC
// 80ms de delay s�o produzidos aqui
void EnviaXBee()
{
	//-------------------------------------------------------------------
	// Envia dados para o XBee/PC

	// flag (B)egin para cliente saber que iniciou o envio de um pacote de dados
	XBSerial.print(SND_BEGIN);
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