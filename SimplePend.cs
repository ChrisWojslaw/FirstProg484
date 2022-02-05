//============================================================================
//SimplePend.cs Defines a class for simulating a simple pendulum
//============================================================================

using System;

namespace Sim
{
    public class SimplePend
    {
        private double len = 1.1; // pedulum length
        private double g = 9.81;  // gravity field strength
        int n = 2;                // number of states
        double z = 2;                // number of states
        private double[] x;       //array of states
        private double[] xi;      // intermediate step for slope
        private double[] f;       // right side of the equation evaulated
        private double[,] Slope;   // Slope of the pendulem

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public SimplePend()
        {
            x = new double[n];
            f = new double[n];
            xi = new double[n];
            Slope = new double[4,n];
           
            x[0] = 1.0; 
            x[1] = 0.0;

        }

        //--------------------------------------------------------------------
        // step: perform one itergration step via Euler's Method
        //--------------------------------------------------------------------
        public void step(double dt)
        {
            //rhsFunc(x,f);
            int i;
        
            for(i=0; i<n; ++i)
            {
                //x[i] = x[i] + f[i] * dt;
                //z = f[i];
                //Console.WriteLine($"{z.ToString()}");
                rhsFunc(x,f);
                if(i==0)
                {
                    Slope[0,i] = f[i];
                    xi[i] = x[i] + Slope[0,i] * .5 * dt;
                    rhsFunc(xi,f);
                    Slope[1,i] = f[i];
                    xi[i] = x[i] + Slope[1,i] * .5 * dt;
                    rhsFunc(xi,f);
                    Slope[2,i] = f[i];
                    xi[i] = x[i] + Slope[2,i]*dt;
                    rhsFunc(xi,f);
                    Slope[3,i] = f[i];
                    x[i] = x[i] + ((.1667*Slope[0,i]) + (.3333 * Slope[1,i]) + (.3333 * Slope[2,i]) + (.1667*Slope[3,i]))*dt;
                }

                if(i==1)
                {
                    Slope[0,i] = f[i];
                    xi[i] = x[i] + Slope[0,i] * .5 * dt;
                    rhsFunc(xi,f);
                    Slope[1,i] = f[i] + .5 * dt;
                    xi[i] = x[i] + Slope[1,i] * .5 * dt;
                    rhsFunc(xi,f);
                    Slope[2,i] = f[i] + .5 * dt;
                    xi[i] = x[i] + Slope[2,i]*dt;
                    rhsFunc(xi,f);
                    Slope[3,i] = f[i] + dt;
                    x[i] = x[i] + ((.1667*Slope[0,i]) + (.3333 * Slope[1,i]) + (.3333 * Slope[2,i]) + (.1667*Slope[3,i]))*dt;
                }
                    //Slope[0,i] = f[i];
                    //xi[i] = x[i] + Slope[0,i] * .5 * dt;
                    //rhsFunc(xi,f);
                    //Slope[1,i] = f[i] * x[i] + .5 * dt;
                    //xi[i] = x[i] + Slope[1,i] * .5 * dt;
                    //rhsFunc(xi,f);
                    //Slope[2,i] = f[i] * x[i] + .5 * dt;
                    //xi[i] = x[i] + Slope[2,i]*dt;
                    //rhsFunc(xi,f);
                    //Slope[3,i] = f[i] * x[i] + dt;
                    //x[i] = x[i] + ((.1667*Slope[0,i]) + (.3333 * Slope[1,i]) + (.3333 * Slope[2,i]) + (.1667*Slope[3,i]))*dt;
            }
                               
        }

        //--------------------------------------------------------------------
        // rhsFunc: function that calculates rhs of pedulum ODE
        //--------------------------------------------------------------------
        public void rhsFunc(double[] st, double[] ff)
        {
            ff[0] = st[1];    // angle dot
            ff[1] = -g/len * Math.Sin(st[0]); // angle
        }

        //--------------------------------------------------------------------
        //getters and setters
        //--------------------------------------------------------------------
        public double L
        {
            get {return(len);}

            set 
            {
                if(value > 0.0)
                len = value;
            }
        }

        public double G
        {
            get {return(g);}

            set 
            {
                if(value >= 0.0)
                g = value;
            }
        }
        
        public double theta
        {
            get {return x[0];}
            set {x[0] = value;}
        }
        public double thetaDot
        {
            get {return x[1];}
            set {x[1] = value;}
        }
    }
}