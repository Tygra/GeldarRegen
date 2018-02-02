#region Disclaimer
/*  
 *  The plugin has some features that I got from other authors.
 *  I don't claim any ownership over those elements which were made by someone else.
 *  The plugin has been customized to fit our need at Geldar,
 *  and because of this, it's useless for anyone else.
 *  I know timers are shit, and If someone knows a way to keep them after relog, tell me.
*/
#endregion

#region Refs
using System;
using System.Data;
using System.IO;
using System.IO.Streams;
using System.ComponentModel;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

//Terraria related refs
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using TShockAPI.DB;
using TShockAPI.Localization;
using Newtonsoft.Json;
using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
#endregion

namespace GeldarRegen
{
    [ApiVersion(2, 1)]
    public class GeldarRegen : TerrariaPlugin
    {
        #region Info & Other things
        public string SavePath = TShock.SavePath;
        public static IDbConnection database;
        static readonly Timer timer = new Timer(1000);
        public override string Name { get { return "Geldar World Regen"; } }
        public override string Author { get { return "Tygra"; } }
        public override string Description { get { return "Geldar World Regen"; } }
        public override Version Version { get { return new Version(0, 1); } }

        public GeldarRegen(Main game)
            : base(game)
        {
            Order = 1;
        }
        #endregion

        #region Initialize
        public override void Initialize()
        {
            if (!Config.ReadConfig())
            {
                TShock.Log.ConsoleError("World Regen Config loading failed. Consider deleting it.");
            }
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Elapsed -= OnUpdate;
                timer.Stop();
            }
            base.Dispose(disposing);    
        }
        #endregion

        #region OnInitialize
        public void Oninitialize(EventArgs args)
        {
            timer.Elapsed += OnUpdate;
            timer.Start();
        }
        #endregion
    }
}
