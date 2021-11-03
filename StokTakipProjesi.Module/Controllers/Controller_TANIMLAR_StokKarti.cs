using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using StokTakipProjesi.Module.BusinessObjects.TANIMLAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StokTakipProjesi.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Controller_TANIMLAR_StokKarti : ViewController
    {
        SimpleAction stokOlustur;
        SimpleAction actionStokAdiDegistir;
        
        Session session;

        public Controller_TANIMLAR_StokKarti()
        {
            InitializeComponent();
            TargetObjectType = typeof(TANIMLAR_StokKarti);

            //stokOlustur = new SimpleAction(this, "btnStokOlustur", PredefinedCategory.Edit);
            //stokOlustur.Caption = "5 Stok Oluştur";
            //stokOlustur.SelectionDependencyType = SelectionDependencyType.Independent;
            //stokOlustur.Execute += stokOlustur_Execute;

            actionStokAdiDegistir = new SimpleAction(this, "btnStokAdDegistir", PredefinedCategory.Edit);
            actionStokAdiDegistir.Caption = "İsim Degistir";
            actionStokAdiDegistir.SelectionDependencyType = SelectionDependencyType.Independent;
            actionStokAdiDegistir.Execute += actionStokAdiDegistir_Execute;

            //var singleChoiceActionStokDegistir = new SingleChoiceAction(this, "btnStokOlusturSingleChoiceIle", "islemler")
            //{
            //    Caption = "Single", ItemType =SingleChoiceActionItemType.ItemIsOperation
            //};
            //singleChoiceActionStokDegistir.SelectionDependencyType = SelectionDependencyType.Independent;
            //singleChoiceActionStokDegistir.Items.Add(new ChoiceActionItem("5StokAç", "deneme"));
            //Actions.Add(singleChoiceActionStokDegistir);
            //singleChoiceActionStokDegistir.Execute += singleChoiceActionStokDegistir_Execute;
            //singleChoiceActionStokDegistir.TargetObjectType = typeof(TANIMLAR_StokKarti);

            var act = new SingleChoiceAction(this, "btnIslemler", PredefinedCategory.Edit);
            act.Caption = "Işlemler";
            act.Items.Add(new ChoiceActionItem
            {
                Id= "StokEkleme",
                Caption = "Stok Ekle"
            });
            act.Items.Add(new ChoiceActionItem
            {
                Id = "aEkle",
                Caption = "A Harfi Ekle"
            });
            act.SelectedItemChanged += Act_SelectedItemChanged;

        }

        private void Act_SelectedItemChanged(object sender, EventArgs e)
        {
            if ((sender as SingleChoiceAction).SelectedItem != null)
            {
                session = (View.CurrentObject as TANIMLAR_StokKarti).Session;

                if ((sender as SingleChoiceAction).SelectedItem.Id == "StokEkleme")
                {
                    TANIMLAR_StokKarti stok;

                    for (int i = 0; i < 5; i++)
                    {
                        stok = new TANIMLAR_StokKarti(session)
                        {
                            Kodu = $"Stok{i + 1}Kodu",
                            Adi = $"Stok{i + 1}",
                            SatistaMi = true,
                            BirimFiyati = 0,
                        };
                        session.Save(stok);
                       
                    }
                    session.CommitTransaction();
                    
                }

                if ((sender as SingleChoiceAction).SelectedItem.Id == "aEkle")
                {
                    var stıokListe = View.SelectedObjects;

                    foreach (TANIMLAR_StokKarti item in stıokListe)
                    {
                        item.Adi += "a";
                    }

                    if (this.ObjectSpace.IsModified)
                    {
                        this.ObjectSpace.CommitChanges();
                        ObjectSpace.Refresh();
                    }
                }
            }
        }
      

        //private void stokOlustur_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{
        ////    var stok = (TANIMLAR_StokKarti)View.CurrentObject;
        ////   session = (View.CurrentObject as TANIMLAR_StokKarti).Session;
        ///
        //TANIMLAR_StokKarti stok;
        //Session session = new Session();
        //session.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //Random rnd = new Random();

        //for (int i = 0; i < 5; i++)
        //{
        //    stok = new TANIMLAR_StokKarti(session)
        //    {
        //        Kodu = rnd.Next(000000, 999999).ToString(),
        //        Adi = $"Stok{i + 1}",
        //        SatistaMi = true,
        //        BirimFiyati = rnd.Next(1, 50),
        //    };
        //    session.Save(stok);
        //    ObjectSpace.Refresh();
        //}



        private void actionStokAdiDegistir_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //var currentObj = View.CurrentObject as TANIMLAR_StokKarti;
            //currentObj.Adi += "a";
            //currentObj.Session.CommitTransection();

            var currentObj = e.CurrentObject as TANIMLAR_StokKarti;

            if (currentObj != null)
            {
                currentObj.Adi += "a";
            }
            if (this.ObjectSpace.IsModified)
            {
                this.ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
