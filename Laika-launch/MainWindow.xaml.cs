using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Laika_launch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum GameState { standBy, launching, launch, launched, record };
    public partial class MainWindow : Window
    {
        //Jäsenmuuttujat (member fields)
        private DispatcherTimer timer;
        private Color propellantColor;
        private Point mousePosition;
        private double mouseLaikaRad; //Apumuuttuja hiiren osoittimen ja laikan sijainnin välisen tangentin tallettamiseen.

        GameState gameState = GameState.standBy;

        Laika laika;
        private double launchForce; // Voisi periaatteessa olla osa Laika-luokkaa
        private double maxLF = 5; // -´´-

        GravitationalObject blackHole;
        private double holeDistance = 0;

        private double maxx = -300;
        private double maxy = -300;

        private int score = 0;
        private int maxScore = 0;

        private Thickness curth;
        private int engineSpeed = 20; //Engine speed

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Pelialustan mitat
            maxx = this.Width - 320 - imgLaika.Width; //Leveydestä vasemman puolen paneelin leveys pois.
            maxy = this.Height - imgLaika.Height;
            //määritellään ns gameloop
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(engineSpeed);
            timer.Tick += new EventHandler(GameTick);
            //Yritetään luoda uusi tietokanta. Tämä tietysti tapahtuu vain kerran ensimmäisessä käynnistyksessä, eikä siitä sen koommin ilmoitella.
            try
            {
                Laika_launch.DB.CreateToSQLite();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            //Ladataan olemassa oleva tietokanta
            try
            {
                dgrTopScores.DataContext = Laika_launch.DB.ReadFromSQLite();
                lblMessages.Text = "Top Scores";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            maxScore = DB.GetHighestScore();
        }

        private void Reset_Game()
        {

            grdBackground.AlignmentX = 0;
            HorizontalAlignment infoPanel = 0;
            stcPnInfopanel.HorizontalAlignment = infoPanel;
            cnvBackPanel.Opacity = 1;
            Thickness lblImagePosition = new Thickness(10, 15, 0, 0);
            Labellaika.Margin = lblImagePosition;
            gameState = GameState.standBy;

            launchForce = 0;

            //Alustetaan laika uudelleen
            laika = new Laika(190, 670, -35, 100); // x, y, angle, propellant
            imgLaika.Opacity = 1;

            //Alustetaan gravitaatiopisteet
            blackHole = new GravitationalObject(650, 450, 10);

            //Käyttöliittymän päivitykset
            pbLaunchForce.Value = 0;
            pbPropellant.Value = 100;
            Thickness newth = new Thickness(laika.X, laika.Y, curth.Right, curth.Bottom);
            imgLaika.Margin = newth;
            Thickness GOthickness = new Thickness(blackHole.X, blackHole.Y, 0, 0);
            imgHole.Margin = GOthickness;
            btnGameState.Content = "Restart";
            txbScore.Text = "Scores: " + score;
            lblMessages.Text = "Top Scores ";


            timer.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            //Laikan asento
            mouseLaikaRad = Math.Atan2(mousePosition.Y - (laika.Y + 70), mousePosition.X - (laika.X + 60));
            imgTransform.Angle = mouseLaikaRad * (180 / Math.PI);

            switch (gameState)
            {
                case GameState.standBy: // Asentoa voidaan muuttaa.

                    lblFlightAssistant.Text = "Click ´n Hold to launch";
                    btnGameState.Content = "Ready to launch";

                    break;
                case GameState.launching:
                    // Asentoa voidaan muuttaa ja lähtölaukaisun voima voidaan asettaa
                    if (launchForce <= maxLF && Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        launchForce += 0.05;
                        pbLaunchForce.Value += 0.5;
                    }
                    break;
                case GameState.launch:
                    // launch tapahtuu vain 1 kertaa ja siitä siirrytään suoraan launched -tilaan.
                    laika.VelocityX += Math.Cos(imgTransform.Angle * Math.PI / 180) * launchForce;
                    laika.VelocityY += Math.Sin(imgTransform.Angle * Math.PI / 180) * launchForce;
                    gameState = GameState.launched;
                    lblFlightAssistant.Text = "";
                    btnGameState.Content = "Launch again";
                    break;
                case GameState.launched:
                    // Asentoa voidaan muuttaa ja lähtölaukaisuvoiman sijasta käytetään "kaasua". Fysiikka otetaan mukaan.

                    holeDistance = Math.Sqrt(Math.Pow(laika.X - blackHole.X, 2) + Math.Pow(laika.Y - blackHole.Y, 2)); //Laikan etäisyys painovoimakeskuksesta - Pythagoraan lause

                    if (laika.Propellant >= laika.MinPropellant && Mouse.LeftButton == MouseButtonState.Pressed) // Lennon aikainen thrust
                    {
                        laika.VelocityX += Math.Cos(imgTransform.Angle * Math.PI / 180) * laika.ThrustForce;
                        laika.VelocityY += Math.Sin(imgTransform.Angle * Math.PI / 180) * laika.ThrustForce;

                        pbPropellant.Value--; // Käyttöliittymälle
                        laika.Propellant--; // Laika-oliolle
                    }

                    laika.VelocityX += Utils.CalculateDeltaV(laika.X - blackHole.X, holeDistance);
                    laika.VelocityY += Utils.CalculateDeltaV(laika.Y - blackHole.Y, holeDistance);

                    laika.X += laika.VelocityX; //Uusi x-sijainti
                    laika.Y += laika.VelocityY;

                    //TODO tarkastelu ettei mene minimin tai maksimin yli/ohi. Annetaan kuitenkin mennä hieman yli rajojen, miellyttävämpää pelillisesti.
                    if (laika.X > maxx || laika.X < -300 || laika.Y > maxy || laika.Y < -300)
                    {
                        timer.Stop();
                        lblFlightAssistant.Text = "         Lost in space";
                        score--;
                    }
                    //Tarkastelu painovoimakeskukseen törmäämisen osalta
                    if (holeDistance < 50)
                    {
                        imgLaika.Opacity = 0;
                        lblFlightAssistant.Text = "Event horizon, no returning";
                        timer.Stop();
                        score--;
                    }
                    //Tarkastelu Marsiin laskeutumisen osalta.
                    if (laika.X < 1350 && laika.X > 1250 && laika.Y > 200 && laika.Y < 300)
                    {
                        if (Math.Abs(laika.VelocityX) + Math.Abs(laika.VelocityY) < 3) //Nopeuksien itseisarvojen summa < 3
                        {
                            lblFlightAssistant.Text = "     Successful landing!";
                            score++;
                            
                        }
                        else
                        {
                            lblFlightAssistant.Text = "         Crashed!";
                            score--;
                        }
                        gameState = GameState.record;
                    }
                    else
                    {
                        Thickness newth = new Thickness(laika.X, laika.Y, curth.Right, curth.Bottom);
                        imgLaika.Margin = newth;
                    }
                    
                    break;
                case GameState.record:
                    //Tehtiinkö uusi ennätys? Avataan tekstikenttä nimensyöttöä varten
                    if (score > maxScore)
                    {
                        maxScore = score;
                        txbScore.Text = $"Scores: {score}   NEW RECORD!";
                        lblMessages.Text = "New Record, Input Name";
                        txbPlayerName.IsEnabled = true;
                        btnGameState.IsEnabled = false; //Restart nappi kiinni kunnes pelaaja on syöttänyt nimensä -> txbPlayer_Keydown event
                    }
                    timer.Stop();
                    break;

            }
            // Propellant progress barin värin ja tekstin muutos
            txbPropellant.Text = $"Propellant {pbPropellant.Value} %";
            if (laika.Propellant > 67) //(Voidaan kytkeä joko propellant tai pbPropellant.Value)
            {
                propellantColor = Colors.ForestGreen;
            }
            else if (laika.Propellant > 25)
            {
                propellantColor = Colors.Orange;
            }
            else
            {
                propellantColor = Colors.Red;
            }
            SolidColorBrush brush = new SolidColorBrush(propellantColor);
            pbPropellant.Foreground = brush;

        }

        private void cnvBackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //Tallennetaan hiiren sijainti
            mousePosition = Mouse.GetPosition(cnvBackPanel);
        }

        private void cnvBackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (gameState)
            {
                case GameState.standBy:
                    gameState = GameState.launching;
                    break;
                case GameState.launched:
                    break;
            }
        }

        private void cnvBackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (gameState)
            {
                case GameState.launching:
                    gameState = GameState.launch;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reset_Game();
        }

        private void txbPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    try
                    {
                        DB.AddToSQLite(txbPlayerName.Text, maxScore);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //Päivitetään myös Top Scores -näkymä
                    try
                    {
                        dgrTopScores.DataContext = Laika_launch.DB.ReadFromSQLite();
                        lblMessages.Text = "New record saved";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    txbPlayerName.IsEnabled = false;
                    btnGameState.IsEnabled = true;
                    break;
            }
        }
    }
}
