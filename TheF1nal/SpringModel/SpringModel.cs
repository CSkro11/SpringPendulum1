//============================================================================
// SpringModel
//============================================================================
using Godot;
using System;

public partial class SpringModel : Node3D
{
	CoilGen coil;
	float L0;    // natural length of spring

	Transform3D tr; // tranform object for the coil
	Vector3 ctrLoc;   // location of coil center
    Vector3 basisVecX; // basis vectors for coil orientation
    Vector3 basisVecY;
    Vector3 basisVecZ;

	Vector3 dVec;     // used for calculating basis vectors
	Vector3 pxRaw;
	Vector3 px;

	//------------------------------------------------------------------------
	// _Ready: Called when the node enters the scene tree for the first time.
	//------------------------------------------------------------------------
	public override void _Ready()
	{
		coil = GetNode<CoilGen>("CoilGen");
		
		tr = new Transform3D();
        ctrLoc = new Vector3();
        basisVecX = new Vector3(1.0f, 0.0f, 0.0f);
        basisVecY = new Vector3(0.0f, 1.0f, 0.0f);
        basisVecZ = new Vector3(0.0f, 0.0f, 1.0f);
        tr.Origin = ctrLoc;
        tr.Basis = Basis.Identity;

		pxRaw = new Vector3(0.0f, 0.0f, 0.0f);
	}

	//------------------------------------------------------------------------
	// GenMesh
	//------------------------------------------------------------------------
	public void GenMesh(float R, float r, float L, float nt,
		int nx = 31, int nd = 10)
	{
		coil.Gen(R, r, L, nt, nx, nd);
		if(coil.ReadyToGo){
			L0 = L;
		}
	}


	//------------------------------------------------------------------------
	// PlaceEndPoints:
	//------------------------------------------------------------------------
	public void PlaceEndPoints(Vector3 endA, Vector3 endB)
	{
		ctrLoc = 0.5f*(endA + endB);
		//Position = ctrLoc;
		Vector3 dVec = endB - endA;
		basisVecX = dVec.Normalized();
		basisVecY = basisVecZ.Cross(basisVecX);
		tr.Origin = ctrLoc;
		tr.Basis = new Basis(basisVecX,basisVecY,basisVecZ);
		Transform = tr;
	}


	//------------------------------------------------------------------------
	// _Called every frame. 'delta' is the elapsed time since the previous frame.
	//------------------------------------------------------------------------
	public override void _Process(double delta)
	{
	}
}
