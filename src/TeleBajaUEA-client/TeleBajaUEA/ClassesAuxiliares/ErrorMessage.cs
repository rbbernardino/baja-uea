using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeleBajaUEA.ClassesAuxiliares
{
    // TODO Tipo de erro deveria ter o conteúdo de "ErrorReason"
    //      e ErrorReason deveria identificar os "details"
    //      certo?? @_@
    public enum ErrorType
    { Error, Info, Warnning, }

    public enum ErrorReason
    {
        NoPortSet,
        PortUnreachable,
        NoAvaiablePort,
        ConnectToCarFailed,
        SendToCarFail,
        ReceiveFromCarFail,
        BackupWillBeSaved,
    }

    public static class ErrorMessage
    {
        public static DialogResult Show(ErrorType type, ErrorReason reason)
        {
            ErrorText text = GetText(reason);
            return DoShow(type, text);
        }

        // função usada quando se deseja especificar o texto de detalhe e usar
        // o título padrão
        public static DialogResult Show(ErrorType type, ErrorReason reason, string details)
        {
            ErrorText text = new ErrorText(GetText(reason).Title, details);
            return DoShow(type, text);
        }

        private static DialogResult DoShow(ErrorType type, ErrorText text)
        {
            string dialogMsg;
            switch (type)
            {
                case ErrorType.Error:
                    dialogMsg =
                        "\t\tERRO\n\n" +
                        text.Title + "\n\n" +
                        text.Details;
                    return MessageBox.Show(
                        dialogMsg, "TeleBajaUEA", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                case ErrorType.Warnning:
                    dialogMsg =
                        "\t\tALERTA\n\n" +
                        text.Title + "\n\n" +
                        text.Details;
                    return MessageBox.Show(
                        dialogMsg, "TeleBajaUEA", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                default:
                    throw new Exception("ErrorType '" +
                        Enum.GetName(typeof(ErrorType), type) +
                        "' não tem mensagem definida.");
            }
        }

        private static ErrorText GetText(ErrorReason reason)
        {
            string title, details;
            switch (reason)
            {
                case ErrorReason.NoPortSet:
                    title = "Nenhuma porta USB configurada!";
                    details =
                        "Existe uma ou mais portas serial disponíveis, porém " +
                        "nenhuma foi configurada, acesse as configurações e " +
                        "escolha uma porta.";
                    return new ErrorText(title, details);

                case ErrorReason.PortUnreachable:
                    title = "Porta '" + Program.Settings.PortXBee + "' inacessível!";
                    details =
                        "Existe uma ou mais portas serial disponíveis, mas " +
                        "a porta configurada não está mais acessível. " +
                        "Por favor, acesse as configurações e escolha uma porta disponível.";
                    return new ErrorText(title, details);

                case ErrorReason.NoAvaiablePort:
                    title = "Nenhuma porta serial disponível!";
                    details = "Conecte o XBee e tente novamente.";
                    return new ErrorText(title, details);

                case ErrorReason.ConnectToCarFailed:
                    title = "Não foi possível conectar-se com o carro.";
                    details = "";
                    return new ErrorText(title, details);

                case ErrorReason.SendToCarFail:
                    title = "Não foi possível enviar uma mensagem ao carro.";
                    details = "";
                    return new ErrorText(title, details);

                case ErrorReason.ReceiveFromCarFail:
                    title = "Não foi possível ler os dados recebidos";
                    details = "";
                    return new ErrorText(title, details);

                case ErrorReason.BackupWillBeSaved:
                    title = "Não se preocupe! Um backup da gravação até o momento" +
                            "será salvo!";
                    details = "";
                    return new ErrorText(title, details);

                default:
                    throw new Exception("ErrorReason '"+
                        Enum.GetName(typeof(ErrorReason), reason) +
                        "' não tem mensagem definida.");
            }
        }

        struct ErrorText
        {
            public string Title { get; }
            public string Details { get; }

            public ErrorText(string pTitle, string pDetails)
            {
                Title = pTitle;
                Details = pDetails;
            }
        }

        // TODO     talvez fosse melhor encapsular essas mensagens acima também,
        //      criando mais uma função Show() de três argumentos, mas desta
        //      vez ao invés de passar "string details", passar a exceção em si
        //      e usar o nome das exceções para definir as chaves dos enums.
        //          Nesse caso poderia ser definido um segundo enum, "InnerErrorReason"
        //      para definir melhor o erro.
        //          Com essa abordagem seria até mesmo possível mandar dados pelo
        //      próprio objeto da exceção, campo Data{} (dicionário).
        //
        // dar opção de exibir a "stack call"?

        [Serializable]
        public class ReceiveDataTimeoutException : Exception
        {
            public override string Message
            {
                get
                {
                    return
                        "O carro demorou muito para responder. Verifique a " + 
                        "conexão e tente novamente.";
                }
            }

            public ReceiveDataTimeoutException() { }
            public ReceiveDataTimeoutException(string message) : base(message) { }
            public ReceiveDataTimeoutException(string message, Exception inner) : base(message, inner) { }
            protected ReceiveDataTimeoutException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context)
            { }
        }

        [System.Serializable]
        public class InvalidProtocolException : Exception
        {
            private char expectedChar;
            private char receivedChar;

            public override string Message { get
                {
                    return
                        "Erro de protocolo, esperava '" + expectedChar + "', mas " +
                        "recebeu '" + receivedChar + "'.";
                }
            }

            public InvalidProtocolException(char expectedChar, char receivedChar)
            {
                this.expectedChar = expectedChar;
                this.receivedChar = receivedChar;
            }

            private InvalidProtocolException(string message) : base(message) { }
            private InvalidProtocolException(string message, Exception inner) : base(message, inner) { }

            protected InvalidProtocolException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context)
            { }
        }
    }
}
