using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public enum GameState
{  Pause , Play , Tutorial }

[Serializable]
public enum ConstructType
{ None, GroundTemplate, Ground, Wall, Furniture, Relax, Work }

[Serializable]
public enum JobType
{ None, Programmer, Graphic, Game_Writer, Sound_Engineer, Marketing, Finance }

[Serializable]
public enum NatureType
{ None, Careful, Quiet, Jolly, Serious, Relaxed, Rush, Prudence }

[Serializable]
public enum CertificateType
{ None, Certificate_01, Certificate_02, Certificate_03 }

[Serializable]
public enum ContractType
{ None, Scenario, Graphic, Module, Support, FullGameProject }

[Serializable]
public enum ContractRank
{ None, S, A, B, C, D}

[Serializable]
public enum ScaleType
{ None, Small, Medium, Large }

[Serializable]
public enum ContractStatus
{ None, Obtain, Claim, Fine }

[Serializable]
public enum EmployeeStatus
{ None, Recruitment, MyEmployee }

[Serializable]
public enum CategorieType
{ None, Game }

[Serializable]
public enum GenreType
{ None, PRG, Sport, Rhythm }

[Serializable]
public enum DevicePlatformType
{ None, PC, Mobile, Web, Console }

[Serializable]
public enum ComponentType
{ None, Feature,Platform,Genre,Graphic,Theme,Camera }

[Serializable]
public enum BussinessModelType
{ None, Digital_Download, Freemium, Subscription, Advertising}

[Serializable]
public enum SentimentType
{ None, Positive, Neutral, Negative }