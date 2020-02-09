using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Certificate : ScriptableObject
{
    public CertificateStructure certificateStructure;
}

public class CertificateStructure
{
    public CertificateType certificateType;
}
