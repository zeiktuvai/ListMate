using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ListMate.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new ListMate.App());
           // string ConnStr = "Server=ec2-54-211-77-238.compute-1.amazonaws.com; Port=5432; User Id=cfntdaptkymihw; Password=3fa0d35f63aedfba0e7a93eb9f9952e3e6ae07a899a08b315b17c0e3081c5283; Database=ddlru0p7beov2d; SSL Mode=Require; Trust Server Certificate=true";

            //Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(ConnStr);
            //try
            //{
            //    conn.Open();
            //} catch(Exception ex)
            //{
                
            //}
            //var command = new Npgsql.NpgsqlCommand("CREATE TABLE Test3 (Name varchar(50));", conn);
            //command.ExecuteNonQuery();

            //conn.Close();
                
        }
    }
}
