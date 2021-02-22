# SELF-CHECKOUT SYSTEM IN VIRTUAL MALL ([v1.0.3](https://github.com/mjk2072/IITP_Mart_Payment/blob/master/HISTORY.md "history")) 
<p align="right">@Updated Sep 09, 2020</p>   

This repository for IITP project. </br></br>

<b>Quick Menu</b>  
&nbsp;&nbsp;&nbsp;&nbsp;[1. Directory ](#1-Structure-of-Directory)</br>
&nbsp;&nbsp;&nbsp;&nbsp;[2. Scenes ](#2-Structure-of-Scenes)</br>
&nbsp;&nbsp;&nbsp;&nbsp;[3. Main Scene: 4_MartScene ](#3-Description-of-Main-Scene-MartScene)</br>
&nbsp;&nbsp;&nbsp;&nbsp;[4. Data Collection ](#4-Data-Collection)</br>

## Prepraing the VR project: Installation
1. Unity 2019.2.18f
2. Tobii XR - SR_Runtime (for eyetracking) [(please check, Step 3)](https://vr.tobii.com/sdk/develop/unity/getting-started/vive-pro-eye/, "eye-tracking")
</br></br>

## 1. Structure of Directory
```diff
Path:
```
* 1_Scenes   
* 2_Scripts   
* 3_Prefabs  
* 4_Models   
* 6_Generic   
* 9_Standard_assets   
* Resources: for Img, Log, Video files    
</br></br>

## 2. Structure of Scenes 
<img width="1436" alt="Flow" src="https://user-images.githubusercontent.com/25002117/92561314-fa8d0a80-f2ae-11ea-94ac-08ee24bcdc29.png">   

*If you want to run Test Scene without any error, you should check 'TestData' Object in 4_MartScene Hierarchy then you can start test scene.*   
*After test,  you should rollback this setting (uncheck 'TestData'). If you don't unchecked 'TestData' object, you will get Error Message.*
</br></br>

## 3. Description of Main Scene: MartScene 
### 3-1. Player
```diff
Path: /4_MartScene/____Player____/
```
* MainCmera   
&nbsp;&nbsp;&nbsp;&nbsp;* Cellphone_canvas:   
&nbsp;&nbsp;&nbsp;&nbsp;* Home_canvas:   
* RealasticWomanL    
* RealasticWomanR    
</br></br>

### 3-2. Description of Mart Hierachy 
```diff
Path: /4_MartScene/Mart/Self-Checkout/___Main_Screen___/___Screen___/
```
<b> For initial screen </b>
* Screen1_initial

<b> For remove an item </b>
* Screen2_uncheck

<b> For discount code </b>
* Screen3_asking

<b> For discount code levels </b>
* Screen4_test
* Screen4_easy
* Screen4_normal
* Screen4_hard:

<b> For ending screen </b>
* Screen5_end
</br></br>

  
### 3-3. Levels of difficulty (about Discount Code)
```diff
Discount Code Path: /2_Scripts/GlobalEnv.cs
Path: /4_MartScene/Mart/Self-Checkout/___Main_Screen___/___Screen___/
```
<img width="927" alt="스크린샷 2020-09-10 오후 2 04 15" src="https://user-images.githubusercontent.com/25002117/92683718-9467bc80-f36e-11ea-899e-2b4ff91edec8.png">

* Test code: A1B1
* Easy Code:
* Normal Code:
* Hard Code:
</br></br>

## 4. Data Collection
Log output Path: /Rsources/Log/

<b>(1) Head</b>
```diff
1-1. Head Tracking Script Path(Position, Ratation): /9_Standard Assets/EventLog/Script/Log/CommonDataLogger.cs
```
* Position: x, y, z
* Rotation: x, y, z, w

<b>(2) Eye</b>   
```diff
2-1. Eye Tracking Script Path(Position, Ratation): /9_Standard Assets/EventLog/Script/Log/CommonDataLogger.cs
2-2. Focusing Check Script Path: /2_Scripts/MartScene/EyetrackingData.cs
```
* Position: x, y, z
* Rotation: x, y, z, w
* Focused object information + object has focus or loses Time (if focused_time > 0.3f, ADD else DEL)
  
<b>(3) Hands</b>   
```diff
3-1. Hands Tracking Script Path(Position, Ratation): /9_Standard Assets/EventLog/Script/Log/CommonDataLogger.cs
3-2. Touching checking Script Path: /4_MartScene/Mart/
```
* Position: x, y, z
* Rotation: x, y, z, w
* Touched object information + Time + Interaction Type (Grab, Release, Touch ...)
