using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkClient :  NetworkBehaviour
{

	[SyncVar]
	public string PlayerName;

	[SyncVar]
	public short PlayerID;

	public bool isPlayer;


	NetManager NetMgr;

	#region Setup and Network Management

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
		NetMgr = GameObject.Find ("! NetworkManager !").GetComponent<NetManager> ();
	}

	void Start ()
	{
		Setup ();
	}

	void Setup ()
	{
		if (hasAuthority)
		{
			NetMgr.LocalPlayer = this;
			this.gameObject.name = "Local Player";
			this.PlayerID = (short)this.netId.Value;
			this.PlayerName = "Player " + PlayerID.ToString ();
			this.isPlayer = this.isLocalPlayer;
		}

		if (!hasAuthority)
		{
			this.enabled = false;
		}

	}

	/// <summary>
	/// Disconnect this Player
	/// </summary>
	public void Disconnect ()
	{

		if (isServer)
		{
			NetworkManager.singleton.StopHost ();
			NetworkServer.Reset ();
		}

		if (isClient)
		{
			NetworkManager.singleton.StopClient ();
		}
	}

	#endregion

	//	void Update ()
	//	{
	//		if (Input.GetKeyDown (KeyCode.Space))
	//			Game.Manager.CreatePlayer ();
	//
	//		if (Input.GetKeyDown (KeyCode.Tab))
	//			CmdTest (this.netId);
	//
	//	}
	//
	//	[ClientRpc]
	//	void RpcTest(NetworkInstanceId instanceID)
	//	{
	//		if (instanceID == this.netId && this.enabled)
	//		{
	//			Debug.Log (" RPC Recieved  - Instance ID : " + instanceID.ToString ());
	//		}
	//	}
	//
	//	[Command]
	//	void CmdTest(NetworkInstanceId instanceID)
	//	{
	//		Debug.Log (" Server Recieved Command Test - Instance ID : " + instanceID.ToString() );
	//		Debug.Log(" Calling RPC on Clients...");
	//		RpcTest (instanceID);
	//	}
	//
	//	[Command]
	//	public void CmdCreatePlayer(string JSON)
	//	{
	//		Debug.Log(" Calling CmdCreatePlayer Command on Server...");
	//		Game.Manager.state.Players.Add (new Player (JSON));
	//		RpcCreatePlayer (JSON);
	//
	//	}
	//
	//	[ClientRpc]
	//	void RpcCreatePlayer(string JSON)
	//	{
	//		Debug.Log(" Calling RpcCreatePlayer on Clients...");
	//		if(!this.isServer)
	//			Game.Manager.state.Players.Add (new Player (JSON));
	//	}


}
