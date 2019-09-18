using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clima.Models;

namespace Clima.Controllers
{
    class CalculosMatematicos
    {
        ///<summary>
        ///Retorna la coordenada X
        ///</summary>
        public double CalcularCoordenadaX(int dia, Planet p)
        {
            double coord_x;

            double angulo = (dia * p.velocidad * p.direccion) % 360;
            double PI_Radianes = Math.Round(angulo * (Math.PI / 180.0), 1);
            coord_x = Math.Round(p.distancia * Math.Cos(PI_Radianes), 1); //Formula para coordenada X


            return coord_x;
        }

        ///<summary>
        ///Retorna la coordenada Y
        ///</summary>
        public double CalcularCoordenadaY(int dia, Planet p)
        {
            double coord_y;

            double angulo = (dia * p.velocidad * p.direccion) % 360;
            double PI_Radianes = Math.Round(angulo * (Math.PI / 180.0), 1);
            coord_y = Math.Round(p.distancia * Math.Sin(PI_Radianes), 1); //Formula para coordenada Y

            return coord_y;

        }

        /// <summary>
        /// Retorna true si los planetas estan alineados, segun la formula: X1 - X2 / Y1 - Y3 = X1-X3 / Y1-Y2
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <param name="coord3"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        public bool EstanAlineados(Coordenadas coord1, Coordenadas coord2, Coordenadas coord3, int dia)
        {
            bool estanAlineados = false;

            var substractAyCy = coord1.Y - coord3.Y; 
            var substractAyBy = coord1.Y - coord2.Y; 

            if (substractAyBy.Equals(0) || substractAyCy.Equals(0))// VERIFICA SI DA CERO PORQUE NO SE PUEDE DIVIDIR ENTRE CERO
            {
                return substractAyBy.Equals(substractAyCy);
            }

            var substractAxBx = coord1.X - coord2.X;
            var substractAxCx = coord1.X - coord3.X;

            var p1 = Math.Round(((substractAxBx / substractAyCy) * 10), 1) / 10.0;
            var p2 = Math.Round(((substractAxCx / substractAyBy) * 10), 1) / 10.0;


            if (p1 == p2)
                estanAlineados = true;
            else
                estanAlineados = false;

            return estanAlineados;
        }

        /// <summary>
        /// Retorna True si el sol esta dentro del triangulo y false si no.
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <param name="coord3"></param>
        /// <param name="sol"></param>
        /// <returns></returns>
        public bool SolDentroDelTriangulo(Coordenadas coord1, Coordenadas coord2, Coordenadas coord3, Coordenadas sol)
        {
            bool estaDentro = false;
            bool orientacionPlanetas = false;
            bool orientacion12ySol = false;
            bool orientacion23ySol = false;
            bool orientacion31ySol = false;


            orientacionPlanetas = OrientacionTriangulo(coord1, coord2, coord3);
            orientacion12ySol = OrientacionTriangulo(coord1, coord2, sol);
            orientacion23ySol = OrientacionTriangulo(coord2, coord3, sol);
            orientacion31ySol = OrientacionTriangulo(coord3, coord1, sol);


            if (orientacionPlanetas == orientacion12ySol && orientacion12ySol == orientacion23ySol && orientacion23ySol == orientacion31ySol) //si todos tienen la misma direccion, el sol esta dentro
                estaDentro = true;
            else
                estaDentro = false;

            return estaDentro;
        }
        

        /// <summary>
        /// Retorna la orientacion del triangulo considerando la siguiente formula
        /// (A1.x - A3.x) * (A2.y - A3.y) - (A1.y - A3.y) * (A2.x - A3.x)
        /// Si el resultado es mayor o igual que 0, la orientación del triángulo será positiva. En caso contrario, la orientación del triángulo será negativa.
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <param name="coord3"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        public bool OrientacionTriangulo(Coordenadas coord1, Coordenadas coord2, Coordenadas coord3)
        {
            bool orientacionPositiva = false;

            double result1 = (coord1.X - coord3.X) * (coord2.Y - coord3.Y);
            double result2 = (coord1.Y - coord3.Y) * (coord2.X - coord3.X);

            double calculoOrientacion = result1 - result2;

            if (calculoOrientacion >= 0)
                orientacionPositiva = true;
            else
                orientacionPositiva = false;

            return orientacionPositiva ;
        }

        /// <summary>
        /// Retorna el perimetro sumando los tres lados del triangulo
        /// </summary>
        /// <param name="coordA"></param>
        /// <param name="coordB"></param>
        /// <param name="coordC"></param>
        public double HallarPerimetro(Coordenadas coordA, Coordenadas coordB, Coordenadas coordC)
        {
            double Perimetro;
            double CoordAyB;
            double CoordByC;
            double CoordCyA;

            CoordAyB = CalcularDistancia(coordA, coordB);
            CoordByC = CalcularDistancia(coordB, coordC);
            CoordCyA = CalcularDistancia(coordC, coordA);

            Perimetro = CoordAyB + CoordByC + CoordCyA;

            return Perimetro;
        }

        /// <summary>
        /// Retorna la distancia entre dos puntos, a partir de sus coordenadas y la siguiente formula:
        /// distancia = Sqrt((xa-xb)^2 + (ya-yb)^2)
        /// </summary>
        /// <param name="coordA"></param>
        /// <param name="coordB"></param>
        /// <returns></returns>
        public double CalcularDistancia(Coordenadas coordA, Coordenadas coordB)
        {
            double distancia;

            distancia = Math.Sqrt(Math.Pow((coordA.X - coordB.X), 2) + Math.Pow((coordA.Y - coordB.Y), 2));

            return distancia;
        }

    }
}
