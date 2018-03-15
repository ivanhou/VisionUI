using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.Infrastructure;
using System.Data;
using System.Data.SQLite;

namespace DataBaseConfig
{
    public class SQLiteConnectionFactory: IDbConnectionFactory
    {
        string _connString = null;
        string _dbPath = null;
        string _dbName = null;

        #region DB

        string _SystemConfig = "CREATE TABLE [SystemConfig] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + "[Language] INTEGER);";

        string _User = "CREATE TABLE [User] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + "[UserName] NVARCHAR(20), [Password] NVARCHAR(20), [Level] INTEGER);";

        string _CameraInfo = "CREATE TABLE [CameraInfo] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + "[StationId] INTEGER, [Name] NVARCHAR(20), [SerialNumber] NVARCHAR(20), [TypeName] NVARCHAR(20),[Exposuer] INTEGER, [Gain] DOUBLE,[Use] BOOL);";

        string _CameraType = "CREATE TABLE [CameraType] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + "[TypeName] NVARCHAR(20));";

        string _Rectangle1 = "CREATE TABLE [Rectangle1] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [Name] NVARCHAR(20), [Row1] DOUBLE, [Column1] DOUBLE, [Row2] DOUBLE, [Column2] DOUBLE);";

        string _Rectangle2 = "CREATE TABLE [Rectangle2] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [Name] NVARCHAR(20), [Row] DOUBLE, [Column] DOUBLE, [Phi] DOUBLE, [Length1] DOUBLE, [Length2] DOUBLE);";

        string _Circle = "CREATE TABLE [Circle] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [Name] NVARCHAR(20), [Row] DOUBLE, [Column] DOUBLE, [Radius] DOUBLE);";

        string _Line = "CREATE TABLE [Line] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [Name] NVARCHAR(20), [RowBegin] DOUBLE, [ColumnBegin] DOUBLE, [RowEnd] DOUBLE, [ColumnEnd] DOUBLE);";

        string _Station1 = "CREATE TABLE [Station1] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [AssayType] INTEGER, [PId] INTEGER);";

        string _Station2 = "CREATE TABLE [Station2] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [AssayType] INTEGER, [PId] INTEGER);";

        string _Station3 = "CREATE TABLE [Station3] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [AssayType] INTEGER, [PId] INTEGER);";

        string _CreateModel = "CREATE TABLE [CreateModel] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [AngleStart] DOUBLE, [AngleExtent] DOUBLE, [NumLevels] INTEGER);";

        string _FindModel = "CREATE TABLE [FindModel] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [Station] INTEGER, [AngleStart] DOUBLE, [AngleExtent] DOUBLE, [MinScore] DOUBLE, [NumMatches] INTEGER, [MaxOverlap] DOUBLE, "
            + " [NumLevels] INTEGER, [Greediness] DOUBLE);";

        string _SubPixel = "CREATE TABLE [SubPixel] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [TypeValues] NVARCHAR(20));";

        string _GetContourPoints = "CREATE TABLE [GetContourPoints] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [Station] INTEGER, [MaxNumPoints] INTEGER, [Threshold] INTEGER, [Sigma] DOUBLE, [Transition] NVARCHAR(20), [Select] NVARCHAR(20),"
            + " [Lenght1] INTEGER , [Lenght2] INTEGER );";

        string _Pos = "CREATE TABLE [GetContourPoints] ([Id] INTEGER NOT NULL ON CONFLICT FAIL PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,"
            + " [MaxNumPoints] INTEGER, [Threshold] INTEGER, [Sigma] DOUBLE, [Transition] NVARCHAR(20), [Select] NVARCHAR(20),"
            + " [Lenght1] INTEGER , [Lenght2] INTEGER );";
        

        string ConnectionStringSQLite
        {
            get
            {
                if (!System.IO.Directory.Exists(this._dbPath))
                {
                    System.IO.Directory.CreateDirectory(this._dbPath);
                }

                string database = this._dbPath + "\\" + this._dbName;
                string connectionString = @"Data Source=" + System.IO.Path.GetFullPath(database) + ";Pooling=true; FailIfMissing=false; UTF8Encoding=True";

                return connectionString;
            }
        }
        

        #endregion

        public SQLiteConnectionFactory(string dbPath, string dbName)
        {
            this._dbPath = dbPath;
            this._dbName = dbName;
            this._connString = @"Data Source=" + System.IO.Path.GetFullPath(this._dbPath + "\\" + this._dbName) + "; Version = 3; Pooling = True; Max Pool Size = 100;";// ";Version =3;Pooling=True;Max Pool Size=100";
            
            creatDB();
        }


        public IDbConnection CreateConnection()
        {
            /* You must add the 'System.Data.SQLite.dll' and implement this method  */

            //throw new NotImplementedException("You must add the System.Data.SQLite.dll and implement the method 'CreateConnection()'.");

            /*
             * If there is an error occurred because can not load the assembly 'SQLite.Interop.dll',the 'SQLite.Interop.dll' in the directories 'x64' and 'x86', you should copy them with folder to the directory 'bin\Debug' and try again.
             */

            SQLiteConnection conn = new SQLiteConnection(this._connString);
            return conn;
        }

        private void creatDB()
        {
            try
            {
                int rct;
                rct = Create_SystemTable();
                rct = Create_UserTable();
                //rct = Create_Rectangle1Table();
                //rct = Create_Rectangle2Table();
                //rct = Create_CircleTable();
                //rct = Create_LineTable();

                rct = Create_CameraTable();
                rct = Create_CameraTypeTable();
                //rct = Create_Station1Table();
                //rct = Create_Station2Table();
                //rct = Create_Station3Table();
                //rct = Create_CreateModelTable();
                //rct = Create_FindModelTable();
                //rct = Create_SubPixelTable();
                //rct = Create_GetContourPointsTable();

            }
            catch (Exception ex)
            {

            }
        }


        private int Create_SystemTable()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._SystemConfig, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_UserTable()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._User, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_CameraTable()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._CameraInfo, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_CameraTypeTable()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._CameraType, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_Rectangle1Table()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Rectangle1, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                
            }

            return ret;
        }

        private int Create_Rectangle2Table()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Rectangle2, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_CircleTable()
        {
            int ret;

            ret = -1;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Circle, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
            
            return ret;
        }

        private int Create_LineTable()
        {
            int ret;

            ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建  

                    //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                    //ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Line, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
            
            return ret;
        }

        private int Create_Station1Table()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Station1, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_Station2Table()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Station2, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_Station3Table()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._Station3, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_CreateModelTable()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._CreateModel, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_FindModelTable()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._FindModel, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_SubPixelTable()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._SubPixel, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        private int Create_GetContourPointsTable()
        {
            int ret = -1;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionStringSQLite))//创建连接  
                {
                    conn.Open();//打开数据库，若文件不存在会自动创建
                    SQLiteCommand cmdCreateTable = new SQLiteCommand(this._GetContourPoints, conn);
                    ret = cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }


    }
}
