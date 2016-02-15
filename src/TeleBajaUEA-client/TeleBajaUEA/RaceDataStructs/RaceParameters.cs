using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA.RaceDataStructs
{
    [Serializable()]
    public struct RaceParameters
    {
        public string pilNome; // max 50char
        public float pilPeso; // 000,00
        public float pilAltura; // 0,00

        public int climaTemp; // 00
        public char clima; // (E)nsolarado; (N)ublado; (C)huvoso
        public char pista; // (M)olhada; (P)arcialmente molhada; (S)eca

        // carro
        public float carPeso;
        public int compTotal, largTotal, altTotal;
        public int vaoLivreF, vaoLivreR;

        // pneu
        public int rodaDiamExternoFront;
        public int rodaDiamExternoRear;
        public int rodaRaioAroFront;
        public int rodaRaioAroRear;
        public int rodaBandagem;
        public float pneuPressao;
        public string pneuMarca;
        public int pneuTipo;

        // outros
        public int distEixo;
        public int bitolaF, bitolaR;
        public float antiDive, antiSquat;
        public int cteMola;
        public int preCargaAmort1;
        public bool preCargaAmortMeio; // se tem ou não o 1/2
        public int rollcenter;
        public float frontToeL, frontToeR;
        public float rearToeL, rearToeR;
        public float frontCamberL, rearCamberR;
        public float caster;
        public float ackermann;
    }
}
