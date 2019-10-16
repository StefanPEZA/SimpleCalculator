using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();

            #region ToolTip Mesage pe butoane
            toolTip1.SetToolTip(Sterge, "Stergere totala");
            toolTip1.SetToolTip(Sterge1, "Sterge doar unul");
            toolTip1.SetToolTip(Radical, "Radical");
            toolTip1.SetToolTip(Inmultire, "Inmultire");
            toolTip1.SetToolTip(Impartire, "Impartire");
            toolTip1.SetToolTip(Plus, "Adunare");
            toolTip1.SetToolTip(Minus, "Scadere/Minus");
            toolTip1.SetToolTip(Factorial, "Factorial");
            toolTip1.SetToolTip(Putere, "Ridica la putere");
            toolTip1.SetToolTip(Virgula, "Virgula");
            toolTip1.SetToolTip(Paranteza1, "Deschide paranteza");
            toolTip1.SetToolTip(Paranteza2, "Inchide paranteza");
            toolTip1.SetToolTip(Modulo, "Modulo/Restul");
            toolTip1.SetToolTip(Egal, "Egal");
            toolTip1.SetToolTip(Zero, "Zero");
            toolTip1.SetToolTip(Unu, "Unu");
            toolTip1.SetToolTip(Doi, "Doi");
            toolTip1.SetToolTip(Trei, "Trei");
            toolTip1.SetToolTip(Patru, "Patru");
            toolTip1.SetToolTip(Cinci, "Cinci");
            toolTip1.SetToolTip(Sase, "Sase");
            toolTip1.SetToolTip(Sapte, "Sapte");
            toolTip1.SetToolTip(Opt, "Opt");
            toolTip1.SetToolTip(Noua, "Noua");
            #endregion
        }

        private void Buttons_onClick(object sender, EventArgs e)
        {

            int U = 0, Lungime;
            Button Buton = (Button)sender;

            #region Scriere pe afisaj(exceptii)
            Lungime = AfisajCalculator.Text.Length;
            if (Buton.Text == "(")
            {
                if (AfisajCalculator.Text != "")
                {
                    if (int.TryParse(AfisajCalculator.Text[Lungime - 1].ToString(), out _))
                    {
                        AfisajCalculator.Text += "*(";
                    }
                    else
                        AfisajCalculator.Text += "(";

                }
                else
                    AfisajCalculator.Text += "(";
            }

            if (Buton.Text == "sqrt" && AfisajCalculator.Text == "")
            {
                AfisajCalculator.Text = "sqrt(";
            }

            if (AfisajCalculator.Text == "" && int.TryParse(Buton.Text.ToString(), out _))
            {
                AfisajCalculator.Text += Buton.Text;
                U = 1;
            }
            #endregion

            Lungime = AfisajCalculator.Text.Length;
            if (AfisajCalculator.Text != "")
            {
                #region Functionalitatea butonului egal (Afisare Raspuns)
                //Actiunea butonului "=";
                if (Buton.Text == "=")
                {
                    //Calculeaza factorialul unui numar;
                    if (AfisajCalculator.Text[Lungime - 1] == '!')
                    {
                        uint result = 1;
                        for (uint i = 1; i <= uint.Parse(AfisajCalculator.Text.Substring(0, Lungime - 1)); i++)
                        {
                            result *= i;
                        }
                        AfisajCalculator.Text = result.ToString();
                    }

                    //Calculeaza operatii normale;
                    else if (!AfisajCalculator.Text.Contains("^") && !AfisajCalculator.Text.Contains("sqrt("))
                    {
                        if (double.TryParse(AfisajCalculator.Text, out _) == false && int.TryParse(AfisajCalculator.Text[Lungime - 1].ToString(), out _) || AfisajCalculator.Text[Lungime - 1] == ')')
                        {
                            AfisajCalculator.Text = AfisajCalculator.Text.Replace(")(", ")*(");
                            try
                            {
                                DataTable calculator = new DataTable();
                                var result = calculator.Compute(AfisajCalculator.Text.Replace(',', '.'), "");
                                AfisajCalculator.Text = result.ToString();
                                calculator.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
                            }

                        }
                    }

                    //Calculeaza ridicarea la putere;
                    else if (AfisajCalculator.Text.Contains("^"))
                    {
                        try
                        {
                            string[] numar = AfisajCalculator.Text.Split('^');
                            double baza = double.Parse(numar[0]);
                            double puterea = double.Parse(numar[1]);
                            var result = Math.Pow(baza, puterea);
                            AfisajCalculator.Text = result.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
                        }
                    }

                    //Calculeaza radicalul unui numar;
                    else if (AfisajCalculator.Text.Contains("sqrt("))
                    {
                        try
                        {
                            char[] split = new char[2] { '(', ')' };
                            string[] str = AfisajCalculator.Text.Split(split);
                            double numar = double.Parse(str[1]);
                            var result = Math.Sqrt(numar);
                            AfisajCalculator.Text = result.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
                        }
                    }
                }
                #endregion

                #region Functionalitate butoane(Scriere/Stergere de pe afisaj)
                //Sterge tot;
                else if (Buton.Text == "CE")
                {
                    AfisajCalculator.Text = "";
                }

                //Sterge doar un caracter;
                else if (Buton.Text == "C")
                {
                    AfisajCalculator.Text = AfisajCalculator.Text.Substring(0, Lungime - 1);
                }

                //Scrie pe afisaj;
                else if (U == 0 && (int.TryParse(AfisajCalculator.Text[Lungime - 1].ToString(), out _) || int.TryParse(Buton.Text.ToString(), out _) || AfisajCalculator.Text[Lungime - 1] == ')') && AfisajCalculator.Text[Lungime - 1] != '!')
                {
                    if (!AfisajCalculator.Text.Contains("^") && Buton.Text != "sqrt")
                    {
                        if (AfisajCalculator.Text[Lungime - 1] == ')' && int.TryParse(Buton.Text, out _))
                        {
                            AfisajCalculator.Text = AfisajCalculator.Text + "*" + Buton.Text;
                        }
                        else
                            AfisajCalculator.Text += Buton.Text;
                    }
                    else if (int.TryParse(Buton.Text, out _) || Buton.Text == ",")
                        AfisajCalculator.Text += Buton.Text;
                }
                AfisajCalculator.Text = AfisajCalculator.Text.Replace(")(", ")*(");
                #endregion

                AfisajCalculator.Focus();
                AfisajCalculator.SelectionStart = AfisajCalculator.Text.Length;
            }

        }


    }
}
