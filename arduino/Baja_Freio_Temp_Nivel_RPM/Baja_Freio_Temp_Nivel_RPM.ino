//Este código lê os valores dos sensores acoplados ao Baja Uea, a saber sensores de temperatura do motor,
//velocidade do móvel, nível de combustível, acionamento do freio e rotação do motor. 
#include "LiquidCrystal.h"
#include "Limits.h"
#include <SoftwareSerial.h>

const int leitorTensao0 = 8;      //Pino analógico que o resistor pull up está conectado.
float valorLeitorTensao0 = 0;     //Freio=="0". Ou seja, toda variável que tem "0" está relacionada ao acionamento do freio
float Voltagem0=0;
char estado[]="ind";
char Ativado = 'F'; // 'F' - False e 'T' - True


const int leitorTensao1 = 9;          //Pino analógico que o sensor de nível de combustível está conectado.
float valorLeitorTensao1 = 0;         //Nível=="1". Ou seja, toda variável que tem "1" está relacionada ao nível
float VR1=0, R1=0, Rmin=150, Rmax=0.1;
int Nivel=0;
int Vnivel=0;


const int leitorTensao2 = 10;      //Pino analógico que o resistor NTC está conectado.
float valorLeitorTensao2 = 0;     //Temperatura=="2". Ou seja, toda variável que tem "2" está relacionada a temperatura.
float VR2=0, R2=0;
int Temp=0;


const int leitorTensao3 = 11;      //Pino analógico que o Conversor FT está conectado.
float valorLeitorTensao3 = 0;     //Rpm=="3". Ou seja, toda variável que tem "3" está relacionada a RPM
float VR3=0;
int rpm3=0;


//Criando um objeto da classe LiquidCrystal e 
//inicializando com os pinos da interface.
LiquidCrystal lcd(51, 49, 43, 41, 39, 37);

// os valores dessas const devem estar exatamente igual aos da aplicação C#
const char SND_START = 'S'; // ENVIA 'S' para PC, dizendo que já pode começar
const char RCV_OK = 'K'; // RECEBE do PC indiciando que pode começar o envio

const char SND_BEGIN = 'B'; // vai começar envio de dados dos sensores (1 pacote)
const char SND_END = 'E'; // finalizou envio (1 pacote)

const int CONN_LED = 22; // led que indica se está conectado com o pc

SoftwareSerial XBSerial(52, 53); // RX (shield: 2), TX (shield: 3)

void setup() 
{
  //Inicializando o LCD e informando o tamanho de 16 colunas e 2 linhas
  //que é o tamanho do LCD JHD 1602byy usado neste projeto.
  lcd.begin(16, 2);
  
  pinMode(CONN_LED, OUTPUT);
  digitalWrite(CONN_LED, LOW);

  XBSerial.begin(9600);
  connectToPC();
}

void connectToPC()
{
	XBSerial.print(SND_START);

	// re-envia START até receber algo
	while (XBSerial.available() < 1)
	{
		XBSerial.print(SND_START);
		delay(500);
	}

	// espera pelo OK do PC para iniciar envio de dados (loop)
	char msg = receive_sync();
	while (msg != RCV_OK)
	{
		msg = receive_sync();
	}

	// ao sair do while inicia loop(), enviando os dados dos sensores
	digitalWrite(CONN_LED, HIGH);
}

char receive_sync()
{
	while (XBSerial.available() < 1)
	{ /* mantém no loop até receber dados */ }

	// ao sair do while terá dado para ler
	return XBSerial.read();
}

void loop() 
{ 
//__________________________________________________________________________________  

     //Acionamento do Freio
     //Leitor de tensão para freio
     valorLeitorTensao0 = analogRead(leitorTensao0); 
     Voltagem0=5*valorLeitorTensao0/1023; //1023 é o valor máximo digital para 5V
     if(Voltagem0>=3)
   {
     estado[1]='o';
     estado[2]='f';
     estado[3]='f';
     Ativado = 'T';
   }
   else
   {
     estado[1]='o';
     estado[2]='n';
     estado[3]=' ';
     Ativado = 'F';
   }

//__________________________________________________________________________________  

     //Temperatura do Motor   
     //Leitor de tensão para temperatura
     valorLeitorTensao2 = analogRead(leitorTensao2); 
     VR2=5*valorLeitorTensao2/1023; //1023 é o valor máximo digital para 5V
     R2=((5*5622)/(5-VR2))-5622;    //VALOR EM OHMS =5622 (valor do resistor 5k6 aproximadamente.
     if(R2>=4200)
     {
       Temp=50;    //para evitar mostrar valores baixos de temperatura, nos quais o sistema não irá atuar, são mostradas as temperaturas acima de 50ºC 
     }
     else
     {
       //valores da curva ajustados para o Arduino por meio de interpolação
       Temp=(((-1.403*0.000000000000001)*pow(R2,5)))+((1.75*(0.00000000001))*pow(R2,4))-((8.404*(0.00000001))*pow(R2,3))+((1.998*(0.0001))*pow(R2,2))-((0.2614)*(R2))+238.6;
     }
     
//__________________________________________________________________________________  

     //Nível de Combustível
     //Leitor de tensão para Nivel
     valorLeitorTensao1 = analogRead(leitorTensao1); 
     VR1=5*valorLeitorTensao1/1023; //1023 é o valor máximo digital para 5V
     R1=((5*2190)/(5-VR1))-2190;    //VALOR EM OHMS =2190 (valor do resistor 2k2 aproximadamente.
     Nivel=(-100*(R1-Rmin))/(Rmin-Rmax);
     Vnivel=Nivel;
     if(Nivel>100 || Nivel<0)
     {
       Vnivel=1000; //falta descobrir como escrever inf ou Nan... Para casos de erro na medição do sensor 
     }

//__________________________________________________________________________________  

    //Rotação do motor e velocidade 
    //Leitor de tensão
     valorLeitorTensao3 = analogRead(leitorTensao3); 
     VR3=5*valorLeitorTensao3/1023; //1023 é o valor máximo digital para 5V
     if(VR3>=0.01)
     {
     rpm3=((3000*VR3))/1.04;//equação que converte tensão em frequencia
     }
     else
     {
     rpm3=0;
     }
//__________________________________________________________________________
  
  // flag (B)egin para cliente saber que iniciou o envio de um pacote de dados
  XBSerial.print(SND_BEGIN);

  //Exibindo valor das variaveis monitoradas no display.
  lcd.clear();           //limpa o display do LCD.
  lcd.print("F:");       //impressao do estado do acionamento (da chave) do freio       
  lcd.print(estado[1]);
  lcd.print(estado[2]);
  lcd.print(estado[3]);
  Ativado = 'T'; // TODO: remover, apenas teste!
  XBSerial.print(Ativado); // envia para XBee o character 'F' (false) ou 'T' (true)
  
  lcd.print(" N:");     //impressao do valor de nivel de combustivel 
  lcd.print(Vnivel);  
  lcd.print("% ");
  Vnivel = 100; // TODO: remover, apenas teste!
  writeInt8(XBSerial, Vnivel); // envia para XBee 1 byte (mais significativo apenas)
  
  lcd.setCursor(0,1);  //posiciona o cursor na coluna 0 linha 1 do LCD.

  lcd.print("T:");     //impressao do valor de temperatura       
  lcd.print(Temp);  
  lcd.print(" ");
  Temp = 300; // TODO: remover, apenas teste!
  writeInt16(XBSerial, Temp);

  lcd.print("Rpm:");   //impressao do valor de rotação do motor (RPM)       
  lcd.print(rpm3);
  rpm3 = 3000; // TODO: remover, apenas teste!
  writeInt16(XBSerial, rpm3);
  
  // flag (E)nd para cliente saber que encerrou o envio de um pacote de dados
  XBSerial.write(SND_END);

  delay(150);
}

// envia um int pequeno (1 bytes) pela porta serial especificada
void writeInt8(SoftwareSerial pSerial, int num)
{
	pSerial.write(lowByte(num)); // envia 1 byte (menos significativo apenas)
}

// envia um int completo (2 bytes) pela porta serial especificada
void writeInt16(SoftwareSerial pSerial, int num)
{
	pSerial.write(lowByte(num)); // envia 1 byte (menos significativo apenas)
	pSerial.write(highByte(num)); // envia 1 byte (mais significativo apenas)
}