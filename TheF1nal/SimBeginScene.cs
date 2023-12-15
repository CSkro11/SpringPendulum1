//Conor Skrobot z1915698 MEE 381 Spring Project
using Godot;
using System;

public partial class SimBeginScene : Node3D
{
	MeshInstance3D anchor;
	MeshInstance3D ball;
	SpringModel spring;
	Label keLabel;
	Label peLabel;
	Label totalLabel;
	PendSim pend;
	double xA, yA, zA; //coords of the anchor
	float length0; //natural length of pendulum
	float length; //length of pendulum
	double angle; //pendulum angle
	double angleInit; //initial pendulum angle
	double time;
	double k; // spring constant
	double mass; //ball mass
	Vector3 endA; //end point of anchor
	Vector3 endB; //end point of ball 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello MEE 381");
		xA = 0.0; yA = 1.2; zA = 0.0;
		anchor = GetNode<MeshInstance3D>("Anchor");
		ball = GetNode<MeshInstance3D>("Ball1");
		spring = GetNode<SpringModel>("SpringModel");
		endA = new Vector3((float)xA, (float)yA, (float)zA);
		anchor.Position = endA;
		keLabel = GetNode<Label>("keLabel");
		peLabel = GetNode<Label>("peLabel");
		totalLabel = GetNode<Label>("totalLabel");
		pend = new PendSim();
		
		length0 = length = 0.9f;
		mass = 1.4;
		k = 90.0;

		spring.GenMesh(0.05f, 0.015f, length, 6.0f,62);

		
		angleInit = Mathf.DegToRad(60.0);
		float angleF = (float)angleInit;
		pend.Angle = (double)angleInit;

		endB.X = endA.X + length*Mathf.Sin(angleF);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z;
		PlacePendulum(endB);
		time = 0.0;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
		//float angleF = (float)Math.Sin(3.0 * time);
		// float angleA = (float)(0.4*time);
		 //length = length0 + 0.3f * (float)Math.Cos(5.0f * time);
		//length = length0 +0.3f * (float)Math.Sin(5.0 * time);

		float angleA = 0.0f;
		//pend.StepRK2(time, delta);
		float angleF = (float)pend.Angle;

		keLabel.Text = "Kinetic Energy:" + pend.KineticEnergy.ToString("0.00");
		peLabel.Text = "Potential Energy" + pend.PotentialEnergy.ToString("0.00");
		totalLabel.Text = "Total Mechanical Energy" + pend.TotalMechanicalEnergy.ToString("0.00");
		float hz = length*Mathf.Sin(angleF);
		
		endB.X = endA.X + hz*Mathf.Cos(angleA);
		endB.Y = endA.Y - length*Mathf.Cos(angleF);
		endB.Z = endA.Z + hz*Mathf.Sin(angleA);
		PlacePendulum(endB);
		time += delta;
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		pend.StepRK4(time,delta);
	}

	
	private void PlacePendulum(Vector3 endBB)
	{
		spring.PlaceEndPoints(endA, endB);
		ball.Position = endBB;
	}
	

}
