/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 11/11/2017 4:35:12 PM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using NetCommand;
using NetworkWrapper;
using System.Collections;

using KayUtils;
using System.IO;


public partial class JGNetwork : MonoBehaviour
{
    static AbstractNetworkClient mClient = null;

    string mIP = "127.0.0.1"; 
    short mPort = 9898;

    void Awake()
    {
        NetworkCommandHandler.Instance.Initialize();
        StartCoroutine(Connect());
    }

    void Update()
    {

    }


    IEnumerator Connect()
    {
        yield return new WaitForSeconds(1);
#if UNITY_EDITOR
        mClient = new NetworkSynClient(mIP, mPort);
#else
        mClient = new NetworkCppClient(mIP, mPort);
#endif
        mClient.Run();
        yield return null;
    }

}
