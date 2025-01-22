[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/P00oz-zc)
**<ins>Note</ins>: Students must update this `README.md` file to be an installation manual or a README file for their own CS403 projects.**

**รหัสโครงงาน:** 66-2_06_lpp-r2

**ชื่อโครงงาน (ไทย):** แอปพลิเคชันเกมเพื่อเสริมสร้างสมาธิสำหรับเด็กประถมต้น

**Project Title (Eng):** APPLICATION GAME TO ENHANCE CONCENTRATION FORELEMENTARY SCHOOL CHILDREN

**อาจารย์ที่ปรึกษาโครงงาน:** ผศ.ดร. ลัมพาพรรณ พันธ์ชูจิตร์ 

**ผู้จัดทำโครงงาน:** (โปรดเขียนข้อมูลผู้จัดทำโครงงานตามฟอร์แมตดังแสดงในตัวอย่างด้านล่าง)
1. นายสุรัติธนาคม เพิ่มสวัสดิ์ชัย  6309681663  suratanakorm.per@dome.tu.ac.th
2. นางสาวกนกอร ทรงวิชัย  6309681622   kanokon.son@dome.tu.ac.th
   
# พัฒนาโดย

 1. Unity
 <a href="https://unity.com/">
	<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Unity_2021.svg/1200px-Unity_2021.svg.png" width="160">
</a>

 2. Visual Studio Code
<a href="https://code.visualstudio.com/">
	<img src="https://www.somkiat.cc/wp-content/uploads/2021/07/vscode.png" width="160">
</a>

### สำหรับแพลตฟอร์ม Android
<img src="https://storage.googleapis.com/gweb-uniblog-publish-prod/images/HeroHomepage_2880x1200.width-1300_oirLRAu.jpg" width="200">


# สารบัญ
 - [ขั้นตอนการติดตั้ง unity](#%E0%B8%82%E0%B8%B1%E0%B9%89%E0%B8%99%E0%B8%95%E0%B8%AD%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%95%E0%B8%B4%E0%B8%94%E0%B8%95%E0%B8%B1%E0%B9%89%E0%B8%87-unity)
 - [ขั้นตอนการนำไปพัฒนาต่อ](#%E0%B8%82%E0%B8%B1%E0%B9%89%E0%B8%99%E0%B8%95%E0%B8%AD%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%99%E0%B8%B3%E0%B9%84%E0%B8%9B%E0%B8%9E%E0%B8%B1%E0%B8%92%E0%B8%99%E0%B8%B2%E0%B8%95%E0%B9%88%E0%B8%AD)
 - [ขั้นตอนการ Port เกม](#%E0%B8%82%E0%B8%B1%E0%B9%89%E0%B8%99%E0%B8%95%E0%B8%AD%E0%B8%99%E0%B8%81%E0%B8%B2%E0%B8%A3-port-%E0%B9%80%E0%B8%81%E0%B8%A1)
 - [วิธีการใช้งาน](#%E0%B8%A7%E0%B8%B4%E0%B8%98%E0%B8%B5%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B9%83%E0%B8%8A%E0%B9%89%E0%B8%87%E0%B8%B2%E0%B8%99)

## ขั้นตอนการติดตั้ง unity

**Unity Version ที่ผู้พัฒนาใช้ :** 2022.3.19f1

 1. ดาวน์โหลดโปรแกรม Unity ได้ [ที่นี่](https://unity.com/download)
 2. เปิด UnityHubSetup ในโฟลเดอร์ Download ขึ้นมา
 3. License Agreement กด "I Agree" > Choose Install Location กด Browse เลือกโฟลเดอร์ที่ต้องการให้เป็นที่อยู่ของโปรแกรม จากนั้นกด "Install" > Completing Unity Hub Setup  [✓] Run Unity Hub กด "Finish"
 4. หน้าต่าง Install Unity Editor เลือก Skip Installation
 5. กด Install Editor
 6. กด Archive > Download Archive > 2022
 7. เลือก Install 2022.3.19f1
 8. [✓] Android Build Support

      L [✓] OpenJDK

      L [✓] Android SDK & NDK tools

      กด Continue

## ขั้นตอนการนำไปพัฒนาต่อ
การ run และเปิด Project ต้องมี git และ Unity Hub
```bash
# Clone this repository
$ git clone https://github.com/ComSciThammasatU/
2567-1-cs403-final-submission-66-2_06_lpp-r2.git

# Open Unity Hub
Add > Add project from disk > Animal Diary
```
> **Setting in Unity Hub**
> 
><img src="https://cdn-icons-png.flaticon.com/512/3247/3247311.png" width="25"> **Game** > *Change* "**Free Aspect**" *to* "**Android 360x800**" 



## ขั้นตอนการ Port เกม
**Open Unity Hub**

**File** > **Build Settings** > *Choose* "**Android**" > **Player Setting** > **Player**


 - Configuration 	
	 - Scripting Backend	- IL2CPP
 - Target Architectures
	 - ARMv7 [✓] 
	 - ARM64  [✓] 

**Build**

## วิธีการใช้งาน
- **ติดตั้งเกมบนมือถือ**
	- ใช้มือถือในการดาวน์โหลด AnimalDiary.apk ที่ git และติดตั้งได้เลย
	- git : `` 2567-1-cs403-final-submission-66-2_06_lpp-r2/Animal Diary.apk``

- **ใช้งานเกมบน Emulator**
	- ดาวน์โหลด AnimalDiary.apk ที่ git จากนั้นคลิกและลาก AnimalDiary.apk มาวางบน Emulator และเปิดใช้งานได้ทันที
	- git: ``2567-1-cs403-final-submission-66-2_06_lpp-r2/Animal Diary.apk``


