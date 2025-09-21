using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JeuBatailleNavale
{
    public partial class FormJeu : Form
    {
        string devinnerCoordonnees; // coordonnée entrée par le joueur
        int devinnerCase; // le numéro de case correspondant à la coordonnée (0 à 24)
        int CaseBateauJoueur1; // la case où est placé le bateau du joueur 1
        int CaseBateauJoueur2; 
        bool tourJoueur1 = true;
        Random rand = new Random();

        // Constructeur
        public FormJeu()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // evite le scintillement
            LancerJeu();
        }
        private void LancerJeu()
        {
            tlpJoueur1.Controls.Clear();
            tlpJoueur2.Controls.Clear();

            for (int i = 0; i < 25; i++)
            { 
                PictureBox tile1 = new PictureBox(); //creation de la picturebox pr representer une case ou pourrait etre un bateau 
                tile1.BackColor = Color.DarkGray;
                tile1.Size = new Size(50, 50);
                tile1.Tag = CaseVersCoordonnees(i); // stocke la coordonnée
                tile1.Click += new EventHandler(Tile_Click); // ajoute un handler
                tlpJoueur1.Controls.Add(tile1);// ajout de la picturebox dans le tableau du joueur 1


                PictureBox tile2 = new PictureBox();
                tile2.BackColor = Color.DarkGray;
                tile2.Size = new Size(50, 50);
                tile2.Tag = CaseVersCoordonnees(i);
                tile2.Click += new EventHandler(Tile_Click);    
                tlpJoueur2.Controls.Add(tile2);
            }

            // placement aléatoire des bateaux
            CaseBateauJoueur1 = rand.Next(0, 25); 
            CaseBateauJoueur2 = rand.Next(0, 25);
            lblInstructions.Text = "Le jeu commence ! Joueur 1 tire en premier.";
            Console.WriteLine("Bateau Joueur 2 placé à la case : " + CaseVersCoordonnees(CaseBateauJoueur2)); // A mettre en commentaire pour la version finale

        }
        private string CaseVersCoordonnees(int numeroCase)
        {
            switch (numeroCase)
            {
                case 0: return "A0";
                case 1: return "A1";
                case 2: return "A2";
                case 3: return "A3";
                case 4: return "A4";

                case 5: return "B0";
                case 6: return "B1";
                case 7: return "B2";
                case 8: return "B3";
                case 9: return "B4";

                case 10: return "C0";
                case 11: return "C1";
                case 12: return "C2";
                case 13: return "C3";
                case 14: return "C4";

                case 15: return "D0";
                case 16: return "D1";
                case 17: return "D2";
                case 18: return "D3";
                case 19: return "D4";

                case 20: return "E0";
                case 21: return "E1";
                case 22: return "E2";
                case 23: return "E3";
                case 24: return "E4";

                default: return "Erreur"; // si le numéro est invalide
            }
        }
        private int ConvertirCoordonnees(string coord)
        {
            switch (coord)
            {
                case "A0": return 0;
                case "A1": return 1;
                case "A2": return 2;
                case "A3": return 3;
                case "A4": return 4;

                case "B0": return 5;
                case "B1": return 6;
                case "B2": return 7;
                case "B3": return 8;
                case "B4": return 9;

                case "C0": return 10;
                case "C1": return 11;
                case "C2": return 12;
                case "C3": return 13;
                case "C4": return 14;

                case "D0": return 15;
                case "D1": return 16;
                case "D2": return 17;
                case "D3": return 18;
                case "D4": return 19;

                case "E0": return 20;
                case "E1": return 21;
                case "E2": return 22;
                case "E3": return 23;
                case "E4": return 24;

                default: return -1; // coordonnée invalide
            }
        }

        private void btnTir_Click(object sender, EventArgs e)
        {
            {
                devinnerCoordonnees = txtCoordonnees.Text.ToUpper().Replace(" ", "");
                devinnerCase = ConvertirCoordonnees(devinnerCoordonnees); // convertit la coordonnée entrée par le joueur en numéro de case

                if (devinnerCase < 0 || devinnerCase > 24)
                {
                    lblInstructions.Text = "Coordonnée invalide. Réessayez.";
                    return;
                }
                // SI cest le tour du joueur 1
                if (tourJoueur1) 
                {
                    tlpJoueur2.Controls[devinnerCase].BackColor = Color.Red;
                    lblInstructions.Text = "Tir manqué de Joueur 1.";

                    if (devinnerCase == CaseBateauJoueur2)
                    {
                        tlpJoueur2.Controls[CaseBateauJoueur2].BackColor = Color.Green;
                        lblInstructions.Text = "Felicitations !";

                        DialogResult result = MessageBox.Show("Joueur 1 a gagné ! Voulez-vous recommencer ?", "Partie terminée", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            txtCoordonnees.Text = ""; // vide le champ de texte
                            LancerJeu(); 
                        }
                        else
                        {
                            Application.Exit(); 
                        }
                        return;
                    }

                }
                else // si cest le tour du joueur 2
                {
                    tlpJoueur1.Controls[devinnerCase].BackColor = Color.Red;
                    lblInstructions.Text = "Tir manqué de Joueur 2.";

                    if (devinnerCase == CaseBateauJoueur1)
                    {
                        tlpJoueur1.Controls[CaseBateauJoueur1].BackColor = Color.Green;
                        lblInstructions.Text = "Felicitations !";

                        DialogResult result = MessageBox.Show("Joueur 2 a gagné ! Voulez-vous recommencer ?", "Partie terminée", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            txtCoordonnees.Text = "";
                            LancerJeu();
                        }
                        else
                        {
                            Application.Exit();
                        }
                        return;
                    }

                }

                tourJoueur1 = !tourJoueur1; // passer au joueur suivant
                lblInstructions.Text += tourJoueur1 ? " À Joueur 1 de jouer." : " À Joueur 2 de jouer.";
                txtCoordonnees.Text = ""; // réinitialise le champ pour le prochain joueur
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            PictureBox caseCliquee = sender as PictureBox; // quelle case a été cliquée

            if (caseCliquee != null && caseCliquee.Tag != null) // vérifie qu'on a bien cliqué sur une case et son tag ne sont pas nuls
            {
                string coord = caseCliquee.Tag.ToString(); //prend ce qui est dans la boîte (Tag) et me le donne sous forme de texte.
                txtCoordonnees.Text = coord; // affiche la coordonnée dans le TextBox
            }
        }



        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void gBTir_Enter(object sender, EventArgs e)
        {

        }

        private void tlpJoueur1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
