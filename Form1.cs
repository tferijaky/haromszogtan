using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Haromszogtan
{
    public partial class Form1 : Form
    {
        struct pont
        {
            public double x;
            public double y;
        }

        pont pontA, pontB, pontC;       //Háromszög csúcsai
        pont bcvektor, acvektor, abvektor;  //Háromszög oldalvektorai
        pont fbc, fac, fab;     //Oldalfelező pontok

        public Form1()
        {
            InitializeComponent();
        }

        static pont kivonas(pont ebbol, pont ezt)
        {
            pont most;
            most.x = ebbol.x - ezt.x;
            most.y = ebbol.y - ezt.y;
            return most;
        }

        private double radtodeg(double ezt)         //Radiánt átváltja szögre.
        {
            return (180 / Math.PI) * ezt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bevezet0.Text = "Háromszögtan - verzió: 0.4";
            kep1.Image = Image.FromFile("minta.jpg");
            bevezet.Text = "Az alkalmazás egy háromszög három csúcsának\n";
            bevezet.Text += "koordinátáiból számol sok mindent.";
            bevezet.Text += "\nJelenlegi számolások:";
            bevezet.Text += "\n- Oldalak vektorai, hosszai és egyenletei";
            bevezet.Text += "\n- Oldalfelezőpontok koordinátái";
            bevezet.Text += "\n- Súlyvonalak vektorai és egyenletei";
            bevezet.Text += "\n- Súlypont";
            bevezet.Text += "\n- Oldalak normálvektorai";
            bevezet.Text += "\n- Oldalfelező merőlegesek egyenletei";
            bevezet.Text += "\n- Köré írt kör középpontja, sugara, egyenlete";
            bevezet.Text += "\n- Szögek";
            bevezet.Text += "\n- Kerület, terület";
            bevezet.Text += "\n- Beírt kör sugara";
            bevezet.Text += "\n\nKattintson a szövegre továbblépésért!";
            bevezet2.Text = "Megálmodta és elkövette: Tamás Ferenc";
            bevezet2.Text += "\nE-mail: tferi@tferi.hu; Weblap: https://tferi.hu/";
            bevezet2.Text += "\nA program szabadon terjeszthető és használható!";
        }

        static string vektorkiir(string szoveg, pont vektor)
        {
            string mit = szoveg + "(";
            mit += Convert.ToString(vektor.x);
            mit += ";";
            mit += Convert.ToString(vektor.y) + ")";
            return (mit);
        }

        static double kerekit(double mit)       //Az itt alkalmazott kerekítés
        {
            return (Math.Round(mit * 10000) / 10000);
        }

        static double vektorhossz(pont mi)      //Vektor hosszának számítása
        {
            double valami = mi.x * mi.x+ mi.y * mi.y;
            return (kerekit(Math.Sqrt(valami)));
        }

        static string vektorhosszkiir(pont vektor)
        {
            string mit = " Hossza: gyök(";
            double valami = vektor.x* vektor.x + vektor.y * vektor.y;
            mit += Convert.ToString(valami) + ")=";
            double gyok = Math.Sqrt(valami);
            mit += Convert.ToString(kerekit(gyok));
            return (mit);
        }

        static string oldalegyenletkiir(string szoveg, pont vektor, pont adott)
        {
            string mit = szoveg;
            mit += Convert.ToString(vektor.y) + "x";
            if (vektor.x>0)
            { mit += "-"; }
            else { mit += "+"; }
            mit += Convert.ToString(Math.Abs(vektor.x)) + "y =";
            double jobboldal = vektor.y * adott.x - vektor.x * adott.y;
            mit += Convert.ToString(vektor.y) + "*";
            if (adott.x < 0)
            { mit += "(" + Convert.ToString(adott.x) + ")"; }
            else { mit += Convert.ToString(adott.x); }
            if (vektor.x > 0)
            { mit += "-"; }
            else { mit += "+"; }
            mit += Convert.ToString(Math.Abs(vektor.x)) + "*";
            if (adott.y < 0)
            { mit += "(" + Convert.ToString(adott.y) + ")"; }
            else { mit += Convert.ToString(adott.y); }
            mit += " => ";
            mit += Convert.ToString(vektor.y) + "x";
            if (vektor.x > 0)
            { mit += "-"; }
            else { mit += "+"; }
            mit += Convert.ToString(Math.Abs(vektor.x)) + "y =";
            mit += Convert.ToString(jobboldal);
            return (mit);
        }

        static string normalegyenletkiir(string szoveg, pont vektor, pont adott)
        {
            string mit = szoveg;
            mit += Convert.ToString(vektor.x) + "x";
            if (vektor.y < 0)
            { mit += "-"; }
            else { mit += "+"; }
            mit += Convert.ToString(Math.Abs(vektor.y)) + "y =";
            double jobboldal = vektor.x * adott.x + vektor.y * adott.y;
            mit += Convert.ToString(vektor.x) + "*";
            if (adott.x < 0)
            { mit += "(" + Convert.ToString(adott.x) + ")"; }
            else { mit += Convert.ToString(adott.x); }
            if (vektor.y < 0)
            { mit += "-"; }
            else { mit += "+"; }
            mit += Convert.ToString(Math.Abs(vektor.y)) + "*";
            if (adott.y < 0)
            { mit += "(" + Convert.ToString(adott.y) + ")"; }
            else { mit += Convert.ToString(adott.y); }
            mit += " => ";
            mit += Convert.ToString(vektor.x) + "x";
            if (vektor.y < 0)
            { mit += "-"; }
            else { mit += "+"; }
            mit += Convert.ToString(Math.Abs(vektor.y)) + "y =";
            mit += Convert.ToString(jobboldal);
            return (mit);
        }

        static pont felezoszamol(pont egy, pont ketto)
        {
            pont vege;
            vege.x = (egy.x + ketto.x) / 2;
            vege.y = (egy.y + ketto.y) / 2;
            return (vege);
        }

        static pont sulypontszamol(pont egy, pont ketto, pont harom)
        {
            pont vege;
            vege.x = (egy.x + ketto.x + harom.x) / 3;
            vege.y = (egy.y + ketto.y + harom.y) / 3;
            vege.x = kerekit(vege.x);
            vege.y = kerekit(vege.y);
            return (vege);
        }

        static pont normalvektor(pont egy, pont ketto)
        {
            pont vege;
            vege.y = ketto.x - egy.x;
            vege.x = egy.y - ketto.y;
            return(vege);
        }

        static double detszamol(double a, double b, double d, double e)
        {
            double most = a * e - b * d;
            return (most);
        }

        private void simaszoveg(object sender, EventArgs e)
        {
            szamoljgomb.Text = "Számoljon";         
        }

        private void proba(object sender, EventArgs e)
        {
            szamoljgomb.Text = "Kattintson!";
        }

        static string koregylkiir(pont c,double r)
        {
            string mit = "(x";
            if (c.x >= 0)
            { mit += "-"; }
            else
            { mit += "+"; }
            mit += Convert.ToString(Math.Abs(c.x));
            mit += ")^2+(y";
            if (c.y >= 0)
            { mit += "-"; }
            else
            { mit += "+"; }
            mit += Convert.ToString(Math.Abs(c.y));
            mit += ")^2 = ";
            mit += Convert.ToString(kerekit(r * r));
            return (mit);
        }

        private void szamoljgomb_Click(object sender, EventArgs e)
        {
            bevezet0.Visible = false;
            bevezet2.Visible = false;
            kep1.Visible = false;
            szamoltak2.Visible = false;
            pontA.x = Convert.ToDouble(ApontX.Text);
            pontA.y = Convert.ToDouble(ApontY.Text);
            pontB.x = Convert.ToDouble(BpontX.Text);
            pontB.y = Convert.ToDouble(BpontY.Text);
            pontC.x = Convert.ToDouble(CpontX.Text);
            pontC.y = Convert.ToDouble(CpontY.Text);
            bcvektor = kivonas(pontC, pontB);
            szamoltak.Visible = true;
            szamoltak.Text = "Számolt adatok:\n";
            szamoltak.Text += vektorkiir("BC vektor: ", bcvektor);
            szamoltak.Text += vektorhosszkiir(bcvektor);
            acvektor = kivonas(pontC, pontA);
            szamoltak.Text += "\n" + vektorkiir("AC vektor: ", acvektor);
            szamoltak.Text += vektorhosszkiir(acvektor);
            abvektor = kivonas(pontB, pontA);
            szamoltak.Text += "\n" + vektorkiir("AB vektor:", abvektor);
            szamoltak.Text += vektorhosszkiir(abvektor);
            szamoltak.Text += "\n----------------------";
            szamoltak.Text += "\n"+ vektorkiir("BC oldal irányvektora: v",bcvektor);
            szamoltak.Text += "\n"+ oldalegyenletkiir("BC oldal egyenlete: ", bcvektor, pontB);
            szamoltak.Text += "\n" + vektorkiir("AC oldal irányvektora: v", acvektor);
            szamoltak.Text += "\n" + oldalegyenletkiir("AC oldal egyenlete: ", acvektor, pontA);
            szamoltak.Text += "\n" + vektorkiir("AB oldal irányvektora: v", abvektor);
            szamoltak.Text += "\n" + oldalegyenletkiir("AB oldal egyenlete: ", abvektor, pontA);
            szamoltak.Text += "\n----------------------";
            fbc = felezoszamol(pontB, pontC);
            szamoltak.Text += "\n" + vektorkiir("BC felezőpontja: ", fbc);
            fac = felezoszamol(pontA, pontC);
            szamoltak.Text += "\n" + vektorkiir("AC felezőpontja: ", fac);
            fab = felezoszamol(pontA, pontB);
            szamoltak.Text += "\n" + vektorkiir("AB felezőpontja: ", fab);
            szamoltak.Text += "\n----------------------";
            pont sabc = kivonas(pontA,fbc);
            szamoltak.Text += "\n" + vektorkiir("BC súlyvonal vektora: v", sabc);
            szamoltak.Text += "\n" + oldalegyenletkiir("BC súlyvonal egyenlete: ",sabc,pontA);
            pont sbac = kivonas(pontB, fac);
            szamoltak.Text += "\n" + vektorkiir("AC súlyvonal vektora: v", sbac);
            szamoltak.Text += "\n" + oldalegyenletkiir("BC súlyvonal egyenlete: ", sbac, pontB);
            pont scab = kivonas(pontC, fab);
            szamoltak.Text += "\n" + vektorkiir("AB súlyvonal vektora: v", scab);
            szamoltak.Text += "\n" + oldalegyenletkiir("BC súlyvonal egyenlete: ", scab, pontC);
            szamoltak.Text += "\n" + vektorkiir("Súlypont: ", sulypontszamol(pontA, pontB, pontC));
            szamoltak.Text += "\n----------------------";
            //szamoljgomb.Visible = false;
            szamoljgomb.Text = "1. lap";
            folytatas.Visible = true;            
        }

        private void folytatas_Click(object sender, EventArgs e)
        {
            szamoltak.Visible = false;
            szamoltak2.Visible = true;
            pont nbc = normalvektor(pontB, pontC);
            szamoltak2.Text = vektorkiir("BC normálvektora: n", nbc);
            szamoltak2.Text += vektorkiir(" és Fa", fbc);
            szamoltak2.Text += "\n" + normalegyenletkiir("BC oldalfelez.merőleges egyl.: ", nbc, fbc);
            double fbcjobb = nbc.x * fbc.x + nbc.y * fbc.y;     //Az előző egyl. jobb oldala
            pont nac = normalvektor(pontA, pontC);
            szamoltak2.Text += "\n" + vektorkiir("AC normálvektora: n", nac);
            szamoltak2.Text += vektorkiir(" és Fb", fac);
            szamoltak2.Text += "\n" + normalegyenletkiir("BC oldalfelez.merőleges egyl.: ", nac, fac);
            double facjobb = nac.x * fac.x + nac.y * fac.y;     //Az előző egyl. jobb oldala
            pont nab = normalvektor(pontA, pontB);
            szamoltak2.Text += "\n" + vektorkiir("AB normálvektora: n", nab);
            szamoltak2.Text += vektorkiir(" és Fc", fab);
            szamoltak2.Text += "\n" + normalegyenletkiir("AB oldalfelez.merőleges egyl.: ", nab, fab);
            szamoltak2.Text += "\n----------------------";
            double detsima = detszamol(nbc.x, nbc.y, nac.x, nac.y);
            if (detsima ==0)
            {
                szamoltak2.Text += "Köréírt kör jelenleg nem számolható.";
            }
            else
            {
                double detx = detszamol(fbcjobb, nbc.y, facjobb, nac.y);
                double dety = detszamol(nbc.x, fbcjobb, nac.x, facjobb);
                pont korkp;                     //Köré írt kör középpontja
                korkp.x = kerekit(detx / detsima);     //Köré írt kör kp. x koordináta
                korkp.y = kerekit(dety / detsima);     //Köré írt kör kp. y koordináta
                szamoltak2.Text += "\n"+ vektorkiir("Köré írt kör középpontja: ",korkp);
                pont sugarvektor = kivonas(korkp, pontA);
                double korsugar = Math.Sqrt(sugarvektor.x * sugarvektor.x + sugarvektor.y * sugarvektor.y); //Kör sugarának hossza
                korsugar = kerekit(korsugar);
                szamoltak2.Text += "\nKöré írt kör sugár " + vektorhosszkiir(sugarvektor);
                szamoltak2.Text += "\nKöré írt kör egyenlete: " + koregylkiir(korkp, korsugar);
            }
            szamoltak2.Text += "\n----------------------";
            szamoltak2.Text += "\n" + vektorkiir("AB vektor:", abvektor) + vektorkiir(" AC vektor: ", acvektor);
            double skalarsz1 = kerekit(abvektor.x * acvektor.x + abvektor.y * acvektor.y);       //A két vektor skaláris szorzata
            szamoltak2.Text += "\ncos alfa=AB*AC/(|AB|*|AC|) = " + Convert.ToString(skalarsz1) + "/(";
            szamoltak2.Text += Convert.ToString(vektorhossz(abvektor)) +"*" + Convert.ToString(vektorhossz(acvektor)) + ") =";
            double cosalfa = kerekit(skalarsz1 / (vektorhossz(abvektor) * vektorhossz(acvektor)));
            szamoltak2.Text += Convert.ToString(cosalfa);
            double alfaszog = radtodeg(Math.Acos(cosalfa));
            szamoltak2.Text += "\nalfa = " + Convert.ToString(kerekit(alfaszog)) + " fok.";
            pont bavektor;
            bavektor.x = -1 * abvektor.x;
            bavektor.y = -1 * abvektor.y;
            szamoltak2.Text += "\n" + vektorkiir("BA vektor:", bavektor) + vektorkiir(" BC vektor: ", bcvektor);
            double skalarsz2 = kerekit(bavektor.x * bcvektor.x + bavektor.y * bcvektor.y);       //A két vektor skaláris szorzata
            szamoltak2.Text += "\ncos béta=BA*BC/(|BA|*|BC|) = " + Convert.ToString(skalarsz2) + "/(";
            szamoltak2.Text += Convert.ToString(vektorhossz(bavektor)) + "*" + Convert.ToString(vektorhossz(bcvektor)) + ") =";
            double cosbeta = kerekit(skalarsz2 / (vektorhossz(bavektor) * vektorhossz(bcvektor)));
            szamoltak2.Text += Convert.ToString(cosbeta);
            double betaszog = radtodeg(Math.Acos(cosbeta));
            szamoltak2.Text += "\nbéta = " + Convert.ToString(kerekit(betaszog)) + " fok.";
            double gammaszog = kerekit(180 - alfaszog - betaszog);
            szamoltak2.Text += "\ngamma = 180-alfa-béta = " + Convert.ToString(gammaszog) + " fok.";
            szamoltak2.Text += "\n----------------------";
            double kerulet = vektorhossz(abvektor) + vektorhossz(acvektor) + vektorhossz(bcvektor);
            szamoltak2.Text += "\nKerület = a+b+c = " + Convert.ToString(kerulet);
            double terulet = vektorhossz(abvektor) * vektorhossz(acvektor) * Math.Sin(alfaszog) / 2;
            szamoltak2.Text += "\nTerület = AB*AC*sin(alfa)/2 = " + Convert.ToString(kerekit(terulet));
            double beirtkr = 2 * terulet / kerulet;
            szamoltak2.Text += "\nBeírt kör sugara = 2*T/K = " + Convert.ToString(kerekit(beirtkr));
        }

        private void bevezet_Click(object sender, EventArgs e)
        {
            bevezet.Visible = false;
            bevezet0.Visible = false;
            kilep.Visible = true;
            Xfelirat.Visible = true;
            Apont.Visible = true;
            Bpont.Visible = true;
            Cpont.Visible = true;
            ApontX.Visible = true;
            ApontY.Visible = true;
            BpontX.Visible = true;
            BpontY.Visible = true;
            CpontX.Visible = true;
            CpontY.Visible = true;
            szamoljgomb.Visible = true;
        }

        private void kilep_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
