using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IgorKrece
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Globalne Promenljive
        string path = ("fajl.csv");
        

        ObservableCollection<Vozac> vozaci = new ObservableCollection<Vozac>();
        ObservableCollection<Proizvodjac> proizvodjaci = new ObservableCollection<Proizvodjac>();

        public ObservableCollection<ProTemp> Modeli { get; set; } = new ObservableCollection<ProTemp>();


        private Image draggedImage;
        private Point startPoint;
        private bool isDraggingListViewItem;
        private bool isDraggingCanvasItem;
        public List<Image> CanvasImages { get; set; }


        #endregion

        #region Main
        public MainWindow()
        {
            InitializeComponent();
            vozaci = UcitajVozaceIzTxt("kolaaa.txt");
            dgVozaci.ItemsSource = vozaci;
            lbVozaci.ItemsSource = vozaci;
            CanvasImages = new List<Image>();
            DataContext = this;

            proizvodjaci = UcitajProizvodjace("proizvodjacii.txt");

            NapraviModele(proizvodjaci, Modeli);
            treeProizvodjaci.ItemsSource = Modeli;
        }
        #endregion

        #region Ucitaj Vozace Iz Txt Fajla
        private ObservableCollection<Vozac> UcitajVozaceIzTxt(string filePath)
        {
            try
            {
                ObservableCollection<Vozac> vozaci = new ObservableCollection<Vozac>();
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] elementi = line.Split(',');

                    if (elementi.Length >= 8)
                    {
                        int Bid = int.Parse(elementi[0]);
                        string ii = elementi[1];
                        string pp = elementi[2];
                        string tt = elementi[3];
                        string nn = elementi[4];
                        int brS = int.Parse(elementi[5]);
                        int brT = int.Parse(elementi[6]);
                        int brP = int.Parse(elementi[7]);
                        string sl = elementi[8];

                        Vozac vozac = new Vozac(Bid, ii, pp, tt, nn, brS, brT, brP, sl);
                        vozaci.Add(vozac);
                    }
                }
                return vozaci;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čitanju fajla: ");
            }
            return vozaci;
        }

        #endregion

        #region Pretrazi Vozace

        private void Pretrazi_Click(object sender, RoutedEventArgs e)
        {
            string pretraga = txtPretraga.Text.ToLower();

            // Filtriraj vozace na osnovu pretrage
            ObservableCollection<Vozac> rezultat = new ObservableCollection<Vozac>();
            if (String.IsNullOrEmpty(txtPretraga.Text) == true)
            {
                rezultat = vozaci;
                dgVozaci.ItemsSource = rezultat;
            }
            else
            {

                foreach (Vozac vozac in dgVozaci.ItemsSource)
                {
                    if (vozac.Ime.ToLower().Contains(pretraga) || vozac.Prezime.ToLower().Contains(pretraga) || vozac.Tim.ToLower().Contains(pretraga))
                    {
                        rezultat.Add(vozac);
                    }
                    if (pretraga == "")
                    {
                        rezultat = vozaci;
                    }
                }
                // Ažuriraj ListBox sa rezultatom pretrage
                dgVozaci.ItemsSource = rezultat;
            }
        }

        #endregion

        #region Eksportuj Vozace
        private void Eksportuj_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter w = new StreamWriter(path, true);

            if (dgVozaci.SelectedItem == null)
            {
                MessageBox.Show("Niste selektovali vozaca!");
            }
            else
            {
                foreach (Vozac v in vozaci)
                {
                    if (dgVozaci.SelectedItem == v)
                    {
                        w.WriteLine($"{v.Ime},{v.Prezime},{v.Tim},{v.BrojPobeda}");
                    }
                }
            }
            w.Flush();
        }
        #endregion

        #region Ucitaj Proizvodjace
        private ObservableCollection<Proizvodjac> UcitajProizvodjace(string filePath)
        {
            try
            {
                ObservableCollection<Proizvodjac> proizvodjaci = new ObservableCollection<Proizvodjac>();
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] elementi = line.Split(',');

                    if (elementi.Length >= 3)
                    {
                        int Bid = int.Parse(elementi[0]);
                        string naz = elementi[1];
                        string sed = elementi[2];
                        string slik = elementi[3];

                        Proizvodjac proo = new Proizvodjac(Bid, naz, sed, slik);
                        proizvodjaci.Add(proo);
                    }
                }
                return proizvodjaci;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čitanju fajla: ");
            }
            return proizvodjaci;
        }



        #endregion

        #region Drop
        private void canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("draggedImage"))
            {
                Image droppedImage = (Image)e.Data.GetData("draggedImage");

                if (!CanvasImages.Contains(droppedImage))
                {
                    Canvas canvas = (Canvas)sender;
                    Point dropPoint = e.GetPosition(canvas);

                    // Create a new Image object to display the dropped image
                    Image newImage = new Image
                    {
                        Source = droppedImage.Source,
                        Width = droppedImage.Width / 2,  // Set the desired width
                        Height = droppedImage.Width / 2,  // Set the desired height
                        DataContext = droppedImage.DataContext  // Associate the Vozac object with the image
                    };

                    // Set the position of the new image on the Canvas
                    Canvas.SetLeft(newImage, dropPoint.X - (newImage.Width / 2));
                    Canvas.SetTop(newImage, dropPoint.Y - (newImage.Height / 2));

                    // Add the new image to the Canvas
                    canvas.Children.Add(newImage);

                    // Enable dragging of the new image on the Canvas
                    newImage.MouseLeftButtonDown += imgVozac_PreviewMouseLeftButtonDown;
                    newImage.MouseMove += imgVozac_PreviewMouseMove;
                   

                    // Add the image to the CanvasImages list
                    CanvasImages.Add(newImage);
                }
            }
        }
        #endregion

        #region Canvas Move
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDraggingListViewItem && e.LeftButton == MouseButtonState.Pressed && draggedImage != null)
            {
               
                isDraggingCanvasItem = true;
                Point position = e.GetPosition(canvas);
                startPoint = position;
                
            }
        }

        #endregion

        #region Img Levi Klik(otvaranje forme za izmenu)
        private void imgVozac_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            draggedImage = (Image)sender;
            startPoint = e.GetPosition(null);
            isDraggingListViewItem = true;

            var source = e.OriginalSource as FrameworkElement;
            if (source != null)
            {
                Image image = FindParentImage(source);
                if (image != null && canvas.Children.Contains(image))
                {
                    Vozac selectedDriver = image.DataContext as Vozac;
                    if (selectedDriver != null)
                    {
                        Izmeni izmeniForm = new Izmeni(selectedDriver);
                        izmeniForm.ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Desni klik(kreiranje menija vozaca)
        private void imgVozac_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as FrameworkElement;
            if (source != null)
            {  //OVDE JE POTREBNA IZMENA TJ DA SE NAPRAVI NOVI USLOV DA SE PROVERI DA LI JE
               //mala SLIKA ili SLIKA STAZE
                Image image = FindParentImage(source);
                if (image != null && image.ActualHeight<200)
                {
                    draggedImage = image;

                    // Create a context menu for the image
                    ContextMenu contextMenu = new ContextMenu();

                    // Create a "Delete Picture" menu item
                    MenuItem deletePictureMenuItem = new MenuItem();
                    deletePictureMenuItem.Header = "Obrisi ikonicu!";
                    deletePictureMenuItem.Click += DeletePictureMenuItem_Click;
                    contextMenu.Items.Add(deletePictureMenuItem);

                    // Create a "Delete Driver" menu item
                    MenuItem deleteDriverMenuItem = new MenuItem();
                    deleteDriverMenuItem.Header = "Obrisi vozaca potpuno!";
                    deleteDriverMenuItem.Click += DeleteDriverMenuItem_Click;
                    contextMenu.Items.Add(deleteDriverMenuItem);
                    

                    // Set the context menu for the image
                    draggedImage.ContextMenu = contextMenu;
                }else if(image!=null && image.ActualHeight > 200)
                {
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem DodajnovogTrkaca = new MenuItem();
                    DodajnovogTrkaca.Header = "Dodaj novog vozaca";
                    DodajnovogTrkaca.Click += DodajNovog_Click;
                    contextMenu.Items.Add(DodajnovogTrkaca);

                   
                }
            }
        }      
        #endregion

        #region Kod za meni vozaca(obrisi ikonicu, obrisi vozaca)
        private Image FindParentImage(FrameworkElement element)
        {
            FrameworkElement parent = element;
            while (parent != null && !(parent is Image))
            {
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            }
            return parent as Image;
        }

        private void DeletePictureMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (draggedImage != null && canvas.Children.Contains(draggedImage))
            {
                canvas.Children.Remove(draggedImage);
                
            }
        }

        private void DeleteDriverMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (draggedImage != null && canvas.Children.Contains(draggedImage))
            {
                // Get the driver associated with the image
                Vozac driver = (Vozac)draggedImage.DataContext;

                // Remove the driver from the collection
                vozaci.Remove(driver);

                // Remove the image from the canvas
                canvas.Children.Remove(draggedImage);
            }
        }
        #endregion

        #region Img Mouse Move
        private void imgVozac_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedImage != null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (isDraggingListViewItem)
                {
                    Point position = e.GetPosition(null);
                    Vector diff = startPoint - position;

                    if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                    {
                        isDraggingListViewItem = false;
                        DataObject dataObject = new DataObject("draggedImage", draggedImage);
                        DragDrop.DoDragDrop(draggedImage, dataObject, DragDropEffects.Move);
                    }
                }
                else if (isDraggingCanvasItem)
                {
                    Point position = e.GetPosition(canvas);

                    double offsetX = position.X - startPoint.X;
                    double offsetY = position.Y - startPoint.Y;

                    Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offsetX);
                    Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offsetY);

                    startPoint = position;
                }
            }
        }


        #endregion

        #region canvas3(drop n mouseMove)
        private void canvas3_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("draggedImage"))
            {
                Image droppedImage = (Image)e.Data.GetData("draggedImage");

                if (!CanvasImages.Contains(droppedImage))
                {
                    Canvas canvas = (Canvas)sender;
                    Point dropPoint = e.GetPosition(canvas);

                    // Create a new Image object to display the dropped image
                    Image newImage = new Image
                    {
                        Source = droppedImage.Source,
                        Width = droppedImage.Width  ,  // Set the desired width
                        Height = droppedImage.Width ,  // Set the desired height
                        DataContext = droppedImage.DataContext  // Associate the Vozac object with the image
                    };

                    // Set the position of the new image on the Canvas
                    Canvas.SetLeft(newImage, dropPoint.X - (newImage.Width / 2));
                    Canvas.SetTop(newImage, dropPoint.Y - (newImage.Height / 2));

                    // Add the new image to the Canvas
                    canvas.Children.Add(newImage);

                    // Enable dragging of the new image on the Canvas
                    newImage.MouseLeftButtonDown += imgProizvodjac_PreviewMouseLeftButtonDown;
                    newImage.MouseMove += imgProizvodjac_PreviewMouseMove;

                    // Add the image to the CanvasImages list
                    CanvasImages.Add(newImage);
                }
            }

        }


        private void canvas3_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDraggingListViewItem && e.LeftButton == MouseButtonState.Pressed && draggedImage != null)
            {
                isDraggingCanvasItem = true;
                Point position = e.GetPosition(canvas);
                startPoint = position;
            }

        }
        #endregion

        #region imgProizvodjac
        private void imgProizvodjac_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedImage != null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (isDraggingListViewItem)
                {
                    Point position = e.GetPosition(null);
                    Vector diff = startPoint - position;

                    if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                    {
                        isDraggingListViewItem = false;
                        DataObject dataObject = new DataObject("draggedImage", draggedImage);
                        DragDrop.DoDragDrop(draggedImage, dataObject, DragDropEffects.Move);
                    }
                }
                else if (isDraggingCanvasItem)
                {
                    Point position = e.GetPosition(canvas);

                    double offsetX = position.X - startPoint.X;
                    double offsetY = position.Y - startPoint.Y;

                    Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offsetX);
                    Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offsetY);

                    startPoint = position;
                }
            }

        }

        private void imgProizvodjac_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            draggedImage = (Image)sender;
            startPoint = e.GetPosition(null);
            isDraggingListViewItem = true;

            var source = e.OriginalSource as FrameworkElement;
            if (source != null)
            {
                Image image = FindParentImage(source);
                if (image != null && canvas3.Children.Contains(image))
                {
                    Proizvodjac sel = image.DataContext as Proizvodjac;
                    if (sel != null)
                    {
                        IzmeniP izmeniForm = new IzmeniP(sel,Modeli);
                        izmeniForm.ShowDialog();
                    }
                }
            }

        }

        #endregion

        #region Meni Treci tab
        private void canvas3_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as FrameworkElement;
            if (source != null)
            {
                
                Image image = FindParentImage1(source);
                if (image != null && image.ActualHeight<200)
                {
                    draggedImage = image;

                    // Create a context menu for the image
                    ContextMenu contextMenu = new ContextMenu();

                    // Create a "Delete Picture" menu item
                    MenuItem DeletePICMenuItem = new MenuItem();
                    DeletePICMenuItem.Header = "Obrisi ikonicu!";
                    DeletePICMenuItem.Click += DeletePICMenuItem_Click;
                    contextMenu.Items.Add(DeletePICMenuItem);

                    // Create a "Delete Driver" menu item
                    MenuItem DeletePROMenuItem = new MenuItem();
                    DeletePROMenuItem.Header = "Obrisi proizvodjaca potpuno!";
                    DeletePROMenuItem.Click += DeletePROMenuItem_Click;
                    contextMenu.Items.Add(DeletePROMenuItem);


                    // Set the context menu for the image
                    draggedImage.ContextMenu = contextMenu;
                }else if(image!=null && image.ActualHeight > 200)
                {
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem DodajNovogProiz = new MenuItem();
                    DodajNovogProiz.Header = "Dodaj novog proizvodjaca";
                    DodajNovogProiz.Click += DodajNovog_Click;
                    contextMenu.Items.Add(DodajNovogProiz);

                    
                }
            }

        }

      

        private Image FindParentImage1(FrameworkElement element)
        {
            FrameworkElement parent = element;
            while (parent != null && !(parent is Image))
            {
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            }
            return parent as Image;
        }

        private void DeletePICMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (draggedImage != null && canvas3.Children.Contains(draggedImage))
            {
                canvas3.Children.Remove(draggedImage);
            }
        }

        private void DeletePROMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (draggedImage != null && canvas3.Children.Contains(draggedImage))
            {
                // Get the driver associated with the image
                Proizvodjac proizvod = (Proizvodjac)draggedImage.DataContext;

                // Remove the driver from the collection
                proizvodjaci.Remove(proizvod);
                foreach (ProTemp pt in Modeli)
                {
                    pt.Pronadji(proizvod.ID);
                }

                // Remove the image from the canvas
                canvas3.Children.Remove(draggedImage);
            }
        }

        #endregion

        #region Metode..
        private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source == canvas || (e.Source is Image image && image.Source is BitmapImage bitmapImage && bitmapImage.UriSource.AbsolutePath.EndsWith("SlikaMape.jpg")))
            {
                // Open the DodajVozaca form
                DodajVozaca dodajVozacaForm = new DodajVozaca(vozaci);
                dodajVozacaForm.ShowDialog();


            }
        }


        private void canvas3_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (e.Source == canvas3 || (e.Source is Image image && image.Source is BitmapImage bitmapImage && bitmapImage.UriSource.AbsolutePath.EndsWith("T.jpg")))
            {
                // Open the DodajVozaca form
                IzmeniProizvodjaca IP = new IzmeniProizvodjaca(Modeli);
                IP.ShowDialog();

            }
        }

        private void DodajNovog_Click(object sender, RoutedEventArgs e)
        {
           
                DodavanjeProizvodjaca noviProzor = new DodavanjeProizvodjaca(Modeli);

                noviProzor.Show();
            

        }


        private void DodajNovogTrkaca_Click(object sender, RoutedEventArgs e)
        {
            NoviTrkac nT = new NoviTrkac(vozaci);

            nT.Show();

        }

        #endregion

        #region Modeli

        void NapraviModele(ObservableCollection<Proizvodjac> proizvodjaci,ObservableCollection<ProTemp> Modeli)
        {
            foreach(Proizvodjac pr in proizvodjaci)
            {
                int brojac = 0;
                foreach(ProTemp pt in Modeli)
                {
                    
                    if (pr.Naziv.Equals(pt.Ime))
                    {
                        brojac++;
                        pt.Dodaj(pr);
                        break;
                    }
                    
             
                }
                if (brojac == 0)
                {
                    ProTemp pp = new ProTemp();
                    pp.Ime = pr.Naziv;
                    pp.Dodaj(pr);
                    Modeli.Add(pp);
                    brojac = 0;
                    
                }
            }
        }

        #endregion





    }
}
