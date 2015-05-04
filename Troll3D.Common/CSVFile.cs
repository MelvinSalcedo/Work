using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace Troll3D.Common
{
    /// <summary>
    /// Lit un fichier CSV
    /// </summary>
    public class CSVFile
    {
        public CSVFile()
        {
            m_Rows = new List<CSVRow>();
            m_HashTable = new Hashtable();
        }

        public CSVFile( string path )
        {
            m_Rows = new List<CSVRow>();
            m_HashTable = new Hashtable();
            LoadFile( path );
        }

        public void InitializeFromString( string content )
        {
            LoadStrings( content );
        }

        public int GetIndex( string column )
        {
            try
            {
                if ( !m_HashTable.ContainsKey( column ) )
                {
                    throw new Exception( " La colonne demandé n'existe pas : " + column );
                }
                return ( int )m_HashTable[column];
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
                return -1;
            }
        }

        public CSVRow GetRow( int x )
        {
            try
            {
                return m_Rows[x];
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
                return null;
            }
        }

        public string GetString( string column, int row )
        {
            try
            {
                return GetString( GetIndex( column ), row );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
                return "";
            }
        }

        public string GetString( int x, int y )
        {
            try
            {
                return m_Rows[y].GetCell( x );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
                return "";
            }
        }

        public int RowCount { get { return m_Rows.Count; } }

        private void LoadStrings( string content )
        {
            try
            {
                bool header = true;
                string currentLine = string.Empty;

                for ( int i = 0; i < content.Length; i++ )
                {
                    if ( content[i] != '\n' )
                    {
                        currentLine += content[i];
                    }
                    else
                    {
                        if ( header )
                        {
                            header = false;
                            LoadHeaders( ChopString( currentLine, ',' ) );
                        }
                        else
                        {
                            AddRow( ChopString( currentLine, ',' ) );
                        }
                        currentLine = string.Empty;
                    }
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( "Erreur dans CSVFiles::LoadStrings : " + e.Message );
            }
        }

        private void LoadFile( string path )
        {
            try
            {
                StreamReader reader = new StreamReader( path );

                string[] str = ChopString( reader.ReadLine(), ',' );
                LoadHeaders( str );

                while ( !reader.EndOfStream )
                {
                    Console.WriteLine( "LoadLine" );
                    str = ChopString( reader.ReadLine(), ',' );
                    AddRow( str );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
            }
        }

        private string[] ChopString( string str, char separator )
        {
            List<string> strings = new List<string>();
            string tempString = "";
            for ( int i = 0; i < str.Length; i++ )
            {
                if ( str[i] == separator )
                {
                    strings.Add( tempString );
                    tempString = "";
                }
                else
                {
                    tempString += str[i];
                }
            }
            strings.Add( tempString );
            return strings.ToArray();
        }

        private void LoadHeaders( string[] strings )
        {
            List<string> headers = new List<string>();
            for ( int i = 0; i < strings.Length; i++ )
            {
                m_HashTable.Add( strings[i], i );
                headers.Add( strings[i] );
            }
            m_Headers = headers.ToArray();
        }

        private void AddRow( string[] strings )
        {
            m_Rows.Add( new CSVRow( this ) );
            for ( int i = 0; i < strings.Length; i++ )
            {
                m_Rows[m_Rows.Count - 1].AddCell( strings[i] );
            }
        }

        private List<CSVRow> m_Rows;
        private string[] m_Headers;          // Représente les headers du fichier CSV

        private Hashtable m_HashTable;
    }

    public class CSVRow
    {
        public CSVRow( CSVFile file )
        {
            m_Cells = new List<string>();
            m_File = file;
        }

        public void AddCell( string str )
        {
            m_Cells.Add( str );
        }

        public string GetCell( string column )
        {
            try
            {
                return GetCell( m_File.GetIndex( column ) );
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
                return "";
            }
        }

        public string GetCell( int i )
        {
            if ( i < m_Cells.Count )
            {
                return m_Cells[i];
            }
            throw ( new Exception( " Can't find Cell" ) );
        }

        private CSVFile m_File;
        private List<string> m_Cells;
    }
}
