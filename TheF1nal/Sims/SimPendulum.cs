using System;
public class SimPendulum
{
 private double L;  // pendulum Length 
 private double g;



 int n;
 double[] x;
 double[] xA;
 double[][]f;

 public SimPendulum()
 {
    L = 0.9;
    g = 9.81;
    
    n = 3;
    x = new double[n];
    xA = new double[n];
    f = new double[4][];
    f[0] = new double[n];
    f[1] = new double[n];

    x[0] = 1.0;
    x[1] = 0.0;
    
 }  

 public void StepEuler(double time, double dt)
 {
    int i;
     RHSFuncPendulum(x, time, f[0]);
     for(i=0; i<n; ++i)
     {
        x[i] += f[0][i]*dt;
     }
 }
 public void StepRK2(double time, double dt)
 {
    int i;

    RHSFuncPendulum(x, time, f[0]);
    for(i=0; i<n; ++i)
    {
        xA[i]= x[i]+f[0][i]*dt;
    }
    RHSFuncPendulum(xA, time+dt, f[1]);
    for(i=0; i<n; ++i)
    {
        x[i] = x[i] + 0.5*(f[0][i]+f[1][i])*dt;
    }
 }
 public void StepRK4(double time, double dt)
 {
    int i;
    RHSFuncPendulum(x, time, f[0]);
    for (i=0; i<n; ++i)
    {
        xA[i] = 0.5*(x[i] + f[0][i])*dt;
    }
    RHSFuncPendulum(xA, time + dt / 2, f[1]);
    for(i = 0; i < n; i++)
    {
        xA[i] = 0.5*(x[i] + f[1][i])*dt;
    }
    RHSFuncPendulum(xA, time + dt / 2, f[2]);
    for (i = 0; i < n; ++i)
    {
        xA[i] = x[i] + f[2][i] * dt;
    }
    RHSFuncPendulum(xA, time + dt, f[3]);

    for (i=0; i < n; ++i)
    {
        x[i] = x[i] + (f[0][i] + 2 * f[1][i] + 2 * f[2][i] + f[3][i])*(dt/6);
    }
 }
 private void RHSFuncPendulum(double[] xx, double t, double[]ff)
 {
    ff[0] = xx[1];
    ff[1] = -g/L*Math.Sin(xx[0]);
    
 }
 public double Angle
 {
    get
    {
        return(x[0]);
    }
    set
    {
        x[0] = value;
    }
 }

}