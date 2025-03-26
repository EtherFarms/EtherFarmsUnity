using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using Thirdweb.Unity;
using System.Numerics;

public class ContractCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void Testing()
    {
        //ThirdwebManager.Instance.Initialize();
        //var wallet = ThirdwebManager.Instance.GetActiveWallet();
        //var contract = await ThirdwebManager.Instance.GetContract("0x55344BFBCE57e185a7a3AA64f24C19C679bac555", 11155111, "optional-contract-abi");
        //var result = await contract.Write(wallet, contract, "get100EtherFarmsToken", BigInteger.Parse("0"));
    }
}
