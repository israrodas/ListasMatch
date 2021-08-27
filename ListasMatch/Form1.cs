using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListasMatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> patrones = new  List<string>{ "*,b,*", "a,*,*", "*,*,c", "foo,bar,baz", "w,x,*,*", "*,x,y,z" };
            List<string> rutas = new List<string> { "w/x/y/z", "a/b/c", "foo/", "foo/bar/", "foo/bar/baz" };
            separa(patrones, rutas);
        }

        public List<List<string>> separaA(List<string> listaPatrones, List<string> listaRutas)
        {
            List<List<string>> listaItems = new List<List<string>>();
            for(int a =0; a <listaPatrones.Count; a++) {
                List<string> items = new List<string>();
                items.AddRange(listaPatrones[a].Split(','));
                listaItems.Add(items);
            }
            List<List<string>> listaItemsR = new List<List<string>>();
            for (int a = 0; a < listaRutas.Count; a++)
            {
                List<string> rutas = new List<string>();
                rutas.AddRange(listaRutas[a].Split('/'));
                listaItemsR.Add(rutas);
            }
            Task<List<List<string>>> respuesta = comparaA(listaItems, listaItemsR);

            return respuesta.Result;

        }

        public async Task<List<List<string>>> comparaA(List<List<string>> listaSeparadaPatrones, List<List<string>> listaSeparadaRutas)
        {
            bool pertenece = false;
            List<List<string>> respuesta = new List<List<string>>();
            for (int a = 0; a < listaSeparadaRutas.Count; a++)
            {
                List<string> listaItems = new List<string>();
                int posicionIngresa = -1, coincidenciasA = 0;
                int tRutas = listaSeparadaRutas[a].Count;
                for (int b = 0; b < listaSeparadaPatrones.Count; b++)
                {
                    int coincidencias = 0;
                    int tPatrones = listaSeparadaPatrones[b].Count;
                    if (tRutas == tPatrones)
                    {
                        for (int a1 = 0; a1 < listaSeparadaRutas[a].Count; a1++)
                        {
                            if (listaSeparadaRutas[a][a1].Equals(listaSeparadaPatrones[b][a1]) || listaSeparadaPatrones[b][a1].Contains("*")) {
                                if (listaSeparadaRutas[a][a1].Equals(listaSeparadaPatrones[b][a1]))
                                    coincidencias++;
                                pertenece = true;
                            }
                            else
                            {
                                pertenece = false;
                                break;
                            }
                        }
                        if (pertenece)
                        {
                            if (coincidenciasA < coincidencias)
                                posicionIngresa = b;
                            coincidenciasA = coincidencias;
                            pertenece = false;
                        }   
                    }
                    else if(b+1 == listaSeparadaPatrones.Count && posicionIngresa < 0)
                    {
                        posicionIngresa = -1;
                        break;
                    }
                }
                if(posicionIngresa > -1) {  
                    respuesta.Add(listaSeparadaPatrones[posicionIngresa]);
                }
                else
                {
                    listaItems.Add("No Match");
                    respuesta.Add(listaItems);
                }
            }
            return respuesta;
        }

        public List<string> separa(List<string> listaPatrones, List<string> listaRutas)
        {
            List<List<string>> listaItems = new List<List<string>>();
            for (int a = 0; a < listaPatrones.Count; a++)
            {
                List<string> items = new List<string>();
                items.AddRange(listaPatrones[a].Split(','));
                listaItems.Add(items);
            }
            List<List<string>> listaItemsR = new List<List<string>>();
            for (int a = 0; a < listaRutas.Count; a++)
            {
                List<string> rutas = new List<string>();
                rutas.AddRange(listaRutas[a].Split('/'));
                listaItemsR.Add(rutas);
            }
            Task<List<string>> respuesta = compara(listaItems, listaItemsR, listaPatrones);

            return respuesta.Result;

        }

        public async Task<List<string>> compara(List<List<string>> listaSeparadaPatrones, List<List<string>> listaSeparadaRutas, List<string> listaPatrones)
        {
            bool pertenece = false;
            List<string> respuesta = new List<string>();
            for (int a = 0; a < listaSeparadaRutas.Count; a++)
            {
                int posicionIngresa = -1, coincidenciasA = 0;
                int tRutas = listaSeparadaRutas[a].Count;
                for (int b = 0; b < listaSeparadaPatrones.Count; b++)
                {
                    int coincidencias = 0;
                    int tPatrones = listaSeparadaPatrones[b].Count;
                    if (tRutas == tPatrones)
                    {
                        for (int a1 = 0; a1 < listaSeparadaRutas[a].Count; a1++)
                        {
                            if (listaSeparadaRutas[a][a1].Equals(listaSeparadaPatrones[b][a1]) || listaSeparadaPatrones[b][a1].Contains("*"))
                            {
                                if (listaSeparadaRutas[a][a1].Equals(listaSeparadaPatrones[b][a1]))
                                    coincidencias++;
                                pertenece = true;
                            }
                            else
                            {
                                pertenece = false;
                                //break;
                            }
                        }
                        if (pertenece)
                        {
                            if (coincidenciasA < coincidencias)
                                posicionIngresa = b;
                            coincidenciasA = coincidencias;
                            pertenece = false;
                        }
                    }
                    else if (b + 1 == listaSeparadaPatrones.Count && posicionIngresa < 0)
                        posicionIngresa = -1;
                }
                if (posicionIngresa > -1)
                    respuesta.Add(listaPatrones[posicionIngresa]);
                else
                    respuesta.Add("No Match");
            }
            return respuesta;
        }
    }
}
