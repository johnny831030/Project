using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace longtermcare.IPBasic
{
    public class SqlIPBasic
    {
        private static string datasource;

        private static string ip_no;
        private static string ip_no_new;
        private static string ip_name;
        private static string ip_sex;
        private static string ip_id;
        private static string ip_birthday;
        private static string ip_country;
        private static string ip_arc;
        private static string ip_passport;
        private static string ip_residedate;
        private static string ip_dnr;
        //private static string ip_blood;
        //private static string ip_born;
        //private static string ip_original;
        private static string ip_height;
        private static string ip_weight;
        private static string ip_permadr;
        private static string ip_nowadr;
        //private static string health_card;
        private static string ip_statues;
        private static string ip_statues_ex;
        private static string ip_barrier;
        private static string check_method;
        private static string ip_belief;
        private static string ip_belief_ex;
        private static string ip_education;
        private static string ip_career;
        private static string ip_career_ex;
        private static string ip_language;
        private static string ip_language_ex;
        private static string ip_language2;
        private static string ip_language_ex2;
        private static string ip_language3;
        private static string ip_language_ex3;
        private static string talk_ability;
        private static string talk_ability_ex;
        private static string ip_money;
        private static string ip_money_ex;
        private static string ip_insurance;
        private static string ip_insurance_ex;
        private static string family_problem;
        private static string stat_infc_hx;
        private static string after_adms_hx;
        private static string ip_photo;
        private static string ip_margin;

        private static string ip_marry;
        private static string ip_marry_ex;
        private static string mate_name;
        private static string mate_duty;
        private static string ip_son;
        private static string ip_daughter;
        private static string in_reason;
        private static string in_reason_ex;
        private static string ip_transfer;
        private static string ip_transfer_ex;
        private static string sign_name;
        private static string sign_relate;
        private static string sign_adr;
        private static string sign_tel;
        private static string sign_tel2;
        private static string sign_tel3;
        private static string re_name;
        private static string re_relate;
        private static string re_adr;
        private static string re_tel;
        private static string re_tel2;
        private static string re_tel3;
        private static string hurry_name;
        private static string hurry_relate;
        private static string hurry_add;
        private static string hurry_tel;
        private static string hurry_tel2;
        private static string hurry_tel3;
        private static string pay_name;
        private static string pay_tel;
        private static string pay_evalute;
        private static string transfer_name;
        private static string ip_identity;
        private static string statINFC;
        private static string afterADMS;
        private static string emr_name;
        private static string emr_relate;
        private static string emr_add;
        private static string emr_tel;
        private static string emr_tel2;
        private static string emr_tel3;
        //訪客限制說明。敬德要求(20160722)
        private static string visitlimit;
        //主簽約者身份證字號。敬德要求(20160722)
        private static string signid;
        //繳款人1關係。敬德要求（20160722）
        private static string payrelate;
        //繳款人2 敬德要求（20160722）
        private static string pay2name;
        private static string pay2relate; //繳款人2關係
        private static string pay2tel;
        //繳款人3 敬德要求（20160722）
        private static string pay3name;
        private static string pay3relate; //繳款人3關係
        private static string pay3tel;


        private static string emr2_name;
        private static string emr2_add;
        private static string emr2_tel;
        private static string emr2_tel2;
        private static string emr2_tel3;
        private static string emr2_relate;
        private static string semail;
        private static string remail;
        private static string hurryemail;
        private static string hurryemail1;
        private static string hurryemail2;
        private static string payemail1;
        private static string scutalkid;
        private static string rcutalkid;
        private static string hurrycutalkid;
        private static string hurrycutalkid1; 
        private static string hurrycutalkid2;
        private static string paycutalkid;


        //膳食習慣
        private static string chklvege_content;
        private static string chklvege1_content;
        private static string chblvege_content;
        private static string pickyfood;
        private static string chklvegetime_content;
        private static string favorbreak; 
        private static string favorlunch;
        private static string favordinner;
        private static string forbid;
        private static string specorder;
        private static string mealbreak;
        private static string mealsnack;
        private static string meallunch;
        private static string mealtea;
        private static string mealdinner;
        private static string mealmnsnack;


        private static string nnselect;
        private static string nncount;
        private static string nnfrom;

        // private static string ipnoselect;
        // private static string Time;

        private static string warning;
        private static string sqlcom = "";
        private static int haveone;

        //以下為訪客
        private static string ip_no_temp;
        private static string ip_emrname;
        private static string ip_emrtel;
        private static string ip_emrtel2;
        private static string ip_emrtel3;
        private static string ip_emrelate;
        private static string ip_visitdate;
        private static string ip_exindate;
        private static string ip_innote;
        private static SqlCommand update;

        public static void connect(string connection_id)
        {
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
        }

        public static string connect1(string connection_id)
        {
            string datasource;
            datasource = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            return datasource;
        }

        public static string FindMaxIpno(string ipyear)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            string ipmaxno = "";
            string findmaxipno = "SELECT MAX_NO FROM IP_MAX_NO WHERE IP_YEAR = '" + ipyear + "'";
            SqlCommand maxipno = new SqlCommand(findmaxipno, conn);
            SqlDataReader max = maxipno.ExecuteReader();
            if (max.Read())
            {
                ipmaxno = max.GetValue(0).ToString();
            }
            conn.Close();
            return ipmaxno;

        }
        internal static void addYearMaxNo(string ipyear)
        {
            SqlConnection connadd = new SqlConnection();
            connadd.Close();
            connadd.ConnectionString = datasource;
            connadd.Open();
            string insertyear = "INSERT INTO IP_MAX_NO(IP_YEAR, MAX_NO) VALUES('"+ipyear+"','0000')";
            SqlCommand insert = new SqlCommand(insertyear, connadd);
            insert.ExecuteNonQuery();
            warning = "新增成功";
            connadd.Close();

        }

        //加入住民基本資料表-個人資料
        public static void AddIPInfo(string ipno, string ipname, string sex, string ipid, string birthday, string country, string arc, string passport, string residedate, string height, string weight, string permadr, string nowadr, string dnr, string statues, string statuesex, string barrier, string checkmethod, string career, string careerex, string belief, string beliefex, string education, string ipMainlanguage, string ipMainlanguageex, string ipSeclanguage, string ipSeclanguageex, string ipThirdlanguage, string ipThirdlanguageex, string talkability, string talkabilityex, string money, string moneyex, string insurance, string insuranceex, string familyproblem, string statinfchx, string statinfc_ex, string afteradmshx, string afteradm_ex, string ipphoto, string createuser, string createdate, string creatTime, string newno, string strip_identity_no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string insertipinfo = "INSERT INTO IP_INFORMATION(IP_NO, IP_NAME, SEX, IP_ID, DOB, IP_CNRY, ARC, PASSPORT, RESIDE_DATE, HEIGHT, WEIGHT, IP_PERM_ADR, NOW_ADR, DNR, STATUES, STATUES_EX, BARRIER, ENTER_METHOD, CAREER, CAREER_EX, BELIEF, BELIEF_EX, EDUCATION, LANGUAGE, LANGUAGE_EX, LANGUAGE2, LANGUAGE_EX2, LANGUAGE3, LANGUAGE_EX3, TALK_ABILITY, TALK_ABILITY_EX, ECO_SOURCE, ECO_SOURCE_EX, INSURANCE, INSURANCE_EX, FMLY_PROBLEM, STAT_INFC_HX, STAT_INFC_HX_EX, AFTER_ADMS_HX, AFTER_ADMS_HX_EX, PHOTO, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable, IP_NO_NEW, IP_IDENTITY_NO) VALUES('" + ipno + "', N'" + ipname + "','" + sex + "','" + ipid + "','" + birthday + "','" + country + "','" + arc + "','" + passport + "','" + residedate + "','" + height + "','" + weight + "','" + permadr + "','" + nowadr + "','" + dnr + "','" + statues + "','" + statuesex + "','" + barrier + "','" + checkmethod + "','" + career + "','" + careerex + "','" + belief + "','" + beliefex + "','" + education + "','" + ipMainlanguage + "','" + ipMainlanguageex + "','" + ipSeclanguage + "','" + ipSeclanguageex + "','" + ipThirdlanguage + "','" + ipThirdlanguageex + "','" + talkability + "','" + talkabilityex + "','" + money + "','" + moneyex + "','" + insurance + "','" + insuranceex + "','" + familyproblem + "','" + statinfchx + "','" + statinfc_ex + "','" + afteradmshx + "','" + afteradm_ex + "','" + ipphoto + "','" + createuser + "','" + createdate + "','" + creatTime + "','Y','" + newno + "','" + strip_identity_no + "')";
            SqlCommand insert = new SqlCommand(insertipinfo, conn);
            insert.ExecuteNonQuery();
            warning = "新增成功";
            conn.Close();
        }
        //加入住民基本資料表-個人資料(敬德)
        public static void AddIPInfo(string ipno, string ipname, string sex, string ipid, string birthday, string country, string arc, string passport, string residedate, string height, string weight, string permadr, string nowadr, string dnr, string statues, string statuesex, string barrier, string checkmethod, string career, string careerex, string belief, string beliefex, string education, string ipMainlanguage, string ipMainlanguageex, string ipSeclanguage, string ipSeclanguageex, string ipThirdlanguage, string ipThirdlanguageex, string talkability, string talkabilityex, string money, string moneyex, string insurance, string insuranceex, string familyproblem, string statinfchx, string statinfc_ex, string afteradmshx, string afteradm_ex, string ipphoto, string createuser, string createdate, string creatTime, string newno, string strip_identity_no, string visitlimit)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string insertipinfo = "INSERT INTO IP_INFORMATION(IP_NO, IP_NAME, SEX, IP_ID, DOB, IP_CNRY, ARC, PASSPORT, RESIDE_DATE, HEIGHT, WEIGHT, IP_PERM_ADR, NOW_ADR, DNR, STATUES, STATUES_EX, BARRIER, ENTER_METHOD, CAREER, CAREER_EX, BELIEF, BELIEF_EX, EDUCATION, LANGUAGE, LANGUAGE_EX, LANGUAGE2, LANGUAGE_EX2, LANGUAGE3, LANGUAGE_EX3, TALK_ABILITY, TALK_ABILITY_EX, ECO_SOURCE, ECO_SOURCE_EX, INSURANCE, INSURANCE_EX, FMLY_PROBLEM, STAT_INFC_HX, STAT_INFC_HX_EX, AFTER_ADMS_HX, AFTER_ADMS_HX_EX, PHOTO, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable, IP_NO_NEW, IP_IDENTITY_NO, VISITRESTRICTION) VALUES('" + ipno + "', N'" + ipname + "','" + sex + "','" + ipid + "','" + birthday + "','" + country + "','" + arc + "','" + passport + "','" + residedate + "','" + height + "','" + weight + "','" + permadr + "','" + nowadr + "','" + dnr + "','" + statues + "','" + statuesex + "','" + barrier + "','" + checkmethod + "','" + career + "','" + careerex + "','" + belief + "','" + beliefex + "','" + education + "','" + ipMainlanguage + "','" + ipMainlanguageex + "','" + ipSeclanguage + "','" + ipSeclanguageex + "','" + ipThirdlanguage + "','" + ipThirdlanguageex + "','" + talkability + "','" + talkabilityex + "','" + money + "','" + moneyex + "','" + insurance + "','" + insuranceex + "','" + familyproblem + "','" + statinfchx + "','" + statinfc_ex + "','" + afteradmshx + "','" + afteradm_ex + "','" + ipphoto + "','" + createuser + "','" + createdate + "','" + creatTime + "','Y','" + newno + "','" + strip_identity_no + "','" + visitlimit + "')";
            SqlCommand insert = new SqlCommand(insertipinfo, conn);
            insert.ExecuteNonQuery();
            warning = "新增成功";
            conn.Close();
        }

        //新增住民基本資料表-家庭資料                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        public static void AddIPFamily(string ipno, string marry, string marryex, string matename, string mateduty, string son, string duaghter, string inreason, string inreasonex, string transfer, string transferex, string signname, string signrelate, string signadr, string signtel, string rename, string rerelate, string readr, string retel, string hurryname, string hurrytel, string payname, string paytel, string payevalute, string transfername, string createuser, string createdate, string creatTime, string sphone2, string sphone3, string rphone2, string rphone3, string erelate, string hurrytel_2, string hurrytel_3, string hurryname1, string hurrytel1, string hurrytel1_2, string hurrytel1_3, string erelate1, string emr1_add, string emr2_add, string hurryname2, string hurryte2, string hurryte2_2, string hurryte2_3, string erelate2, string emr3_add, string semail, string scutalkid, string remail, string rcutalkid, string hurryemail, string hurrycutalkid, string hurryemail1, string hurrycutalkid1, string hurryemail2, string hurrycutalkid2, string payemail1, string paycutalkid)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string insertipfamily = "INSERT INTO IP_FAMILY(IP_NO, MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER, IN_REASON, IN_REASON_EX, TRANSFER_UNT, TRANSFER_EX, SIGN_NAME, SIGN_RELATE, SIGN_ADD, SIGN_TEL, INVO_NAME, INVO_RELATE,INVO_ADD, INVO_TEL, EMR_NAME, EMR_TEL, PAY_NAME, PAY_TEL, PAY_EVALUTE, TRANSFER_NAME, CREATE_USER, CREATE_DATE, CREATE_TIME, SIGN_TEL2, SIGN_TEL3, INVO_TEL2, INVO_TEL3, EMR_RELATE, EMR_TEL2, EMR_TEL3 ,AccountEnable, EMR1_NAME, EMR1_TEL, EMR1_TEL2, EMR1_TEL3, EMR1_RELATE, EMR_ADD, EMR1_ADD,EMR2_NAME,EMR2_ADD,EMR2_TEL,EMR2_TEL2,EMR2_TEL3,EMR2_RELATE, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID ) VALUES('" + ipno + "','" + marry + "','" + marryex + "', N'" + matename + "','" + mateduty + "','" + son + "','" + duaghter + "','" + inreason + "','" + inreasonex + "','" + transfer + "','" + transferex + "', N'" + signname + "','" + signrelate + "','" + signadr + "','" + signtel + "', N'" + rename + "','" + rerelate + "','" + readr + "','" + retel + "', N'" + hurryname + "','" + hurrytel + "', N'" + payname + "','" + paytel + "','" + payevalute + "', N'" + transfername + "','" + createuser + "','" + createdate + "','" + creatTime + "','" + sphone2 + "','" + sphone3 + "','" + rphone2 + "','" + rphone3 + "','" + erelate + "','" + hurrytel_2 + "','" + hurrytel_3 + "','Y', N'" + hurryname1 + "','" + hurrytel1 + "','" + hurrytel1_2 + "','" + hurrytel1_3 + "','" + erelate1 + "','" + emr1_add + "','" + emr2_add + "', N'" + hurryname2 + "','" + emr3_add + "','" + hurryte2 + "','" + hurryte2_2 + "','" + hurryte2_3 + "','" + erelate2 + "','" + semail + "','" + remail + "','" + hurryemail + "','" + hurryemail1 + "','" + hurryemail2 + "','" + payemail1 + "','" + scutalkid + "','" + rcutalkid + "','" + hurrycutalkid + "','" + hurrycutalkid1 + "','" + hurrycutalkid2 + "','" + paycutalkid + "')";  
            SqlCommand insert = new SqlCommand(insertipfamily, conn);
            insert.ExecuteNonQuery();
            warning = "新增成功";
            conn.Close();
        }
        //新增住民基本資料表-家庭資料(敬德)
        public static void AddIPFamily(string ipno, string marry, string marryex, string matename, string mateduty, string son, string duaghter, string inreason, string inreasonex, string transfer, string transferex, string signname, string signrelate, string signadr, string signtel, string rename, string rerelate, string readr, string retel, string hurryname, string hurrytel, string payname, string paytel, string payevalute, string transfername, string createuser, string createdate, string creatTime, string sphone2, string sphone3, string rphone2, string rphone3, string erelate, string hurrytel_2, string hurrytel_3, string hurryname1, string hurrytel1, string hurrytel1_2, string hurrytel1_3, string erelate1, string emr1_add, string emr2_add, string hurryname2, string hurryte2, string hurryte2_2, string hurryte2_3, string erelate2, string emr3_add, string semail, string scutalkid, string remail, string rcutalkid, string hurryemail, string hurrycutalkid, string hurryemail1, string hurrycutalkid1, string hurryemail2, string hurrycutalkid2, string payemail1, string paycutalkid, string signid, string payrelate, string pay2name, string pay2relate, string pay2tel, string pay3name, string pay3relate, string pay3tel)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string insertipfamily = "INSERT INTO IP_FAMILY(IP_NO, MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER, IN_REASON, IN_REASON_EX, TRANSFER_UNT, TRANSFER_EX, SIGN_NAME, SIGN_RELATE, SIGN_ADD, SIGN_TEL, INVO_NAME, INVO_RELATE,INVO_ADD, INVO_TEL, EMR_NAME, EMR_TEL, PAY_NAME, PAY_TEL, PAY_EVALUTE, TRANSFER_NAME, CREATE_USER, CREATE_DATE, CREATE_TIME, SIGN_TEL2, SIGN_TEL3, INVO_TEL2, INVO_TEL3, EMR_RELATE, EMR_TEL2, EMR_TEL3 ,AccountEnable, EMR1_NAME, EMR1_TEL, EMR1_TEL2, EMR1_TEL3, EMR1_RELATE, EMR_ADD, EMR1_ADD,EMR2_NAME,EMR2_ADD,EMR2_TEL,EMR2_TEL2,EMR2_TEL3,EMR2_RELATE, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID, SIGN_ID, PAY_RELATE, PAY2_NAME, PAY2_RELATE, PAY2_TEL, PAY3_NAME, PAY3_RELATE, PAY3_TEL ) VALUES('" + ipno + "','" + marry + "','" + marryex + "', N'" + matename + "','" + mateduty + "','" + son + "','" + duaghter + "','" + inreason + "','" + inreasonex + "','" + transfer + "','" + transferex + "', N'" + signname + "','" + signrelate + "','" + signadr + "','" + signtel + "', N'" + rename + "','" + rerelate + "','" + readr + "','" + retel + "', N'" + hurryname + "','" + hurrytel + "', N'" + payname + "','" + paytel + "','" + payevalute + "', N'" + transfername + "','" + createuser + "','" + createdate + "','" + creatTime + "','" + sphone2 + "','" + sphone3 + "','" + rphone2 + "','" + rphone3 + "','" + erelate + "','" + hurrytel_2 + "','" + hurrytel_3 + "','Y', N'" + hurryname1 + "','" + hurrytel1 + "','" + hurrytel1_2 + "','" + hurrytel1_3 + "','" + erelate1 + "','" + emr1_add + "','" + emr2_add + "', N'" + hurryname2 + "','" + emr3_add + "','" + hurryte2 + "','" + hurryte2_2 + "','" + hurryte2_3 + "','" + erelate2 + "','" + semail + "','" + remail + "','" + hurryemail + "','" + hurryemail1 + "','" + hurryemail2 + "','" + payemail1 + "','" + scutalkid + "','" + rcutalkid + "','" + hurrycutalkid + "','" + hurrycutalkid1 + "','" + hurrycutalkid2 + "','" + paycutalkid + "','" + signid + "','" + payrelate + "','" + pay2name + "','" + pay2relate + "','" + pay2tel + "','" + pay3name + "','" + pay3relate + "','" + pay3tel + "')";
            SqlCommand insert = new SqlCommand(insertipfamily, conn);
            insert.ExecuteNonQuery();
            warning = "新增成功";
            conn.Close();
        }
        //, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
        //  semail, remail, hurryemail, hurryemail1, hurryemail2, payemail1, scutalkid, rcutalkid, hurrycutalkid, hurrycutalkid1, hurrycutalkid2, paycutalkid
        /*
         SELECT      IP_NO, MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER, IN_REASON, IN_REASON_EX, TRANSFER_UNT, TRANSFER_EX, SIGN_NAME, 
                      SIGN_RELATE, SIGN_ADD, SIGN_TEL, SIGN_TEL2, SIGN_TEL3, INVO_NAME, INVO_RELATE, INVO_ADD, INVO_TEL, INVO_TEL2, INVO_TEL3, INVO_NAME_2, 
                      INVO_RELATE_2, INVO_ADD_2, INVO_TEL_2, INVO_TEL_22, INVO_TEL_23, INVO_NAME_3, INVO_RELATE_3, INVO_ADD_3, INVO_TEL_3, INVO_TEL_32, INVO_TEL_33,
                       EMR_NAME, EMR_ADD, EMR_TEL, EMR_TEL2, EMR_TEL3, PAY_NAME, PAY_TEL, PAY_TEL2, PAY_TEL3, PAY_EVALUTE, TRANSFER_NAME, CREATE_USER, 
                      CREATE_DATE, CREATE_TIME, OP_USER, OP_DATE, OP_TIME, AccountEnable, EMR_RELATE, EMR1_NAME, EMR1_ADD, EMR1_TEL, EMR1_TEL2, EMR1_TEL3, 
                      EMR1_RELATE, SIGN_EMAIL, INVO1_EMAIL, INVO2_EMAIL, INVO3_EMAIL, EMR_EMAIL, EMR1_EMAIL, PAY_EMAIL, EMR2_NAME, EMR2_ADD, EMR2_TEL, 
                      EMR2_TEL2, EMR2_TEL3, EMR2_RELATE, EMR2_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, INVO2_CUTALK_MID, INVO3_CUTALK_MID, EMR_CUTALK_MID, 
                      EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
        FROM         IP_FAMILY
        */
        //新增住民膳食偏好
        public static void AddIPMealfavor(string ipno, string chklvege_content, string chblvege_content, string chklvegetime_content, string favorbreak, string favorlunch, string favordinner, string forbid, string specorder, string mealbreak, string mealsnack, string meallunch, string mealtea, string mealdinner, string mealmnsnack, string createuser, string createdate, string creatTime, string pickyfood)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            string creatTime_c = creatTime.Substring(0, 5).Replace(":", "");
            string insertipmeal = "INSERT INTO IP_MEALFAVOR(IP_NO, CARNIVORE1, CARNIVORE2, PICKYFOOD, VEGETABLE, BREAKFASTMAINFOOD, LUNCHMAINFOOD, DINNERMAINFOOD, AVOID, INSTRUCT, BREAKFASTREQ, SNACKREQ, LUNCHREQ, TEABREAKREQ, DINNERREQ, MNSNACKREQ, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable) VALUES('" + ipno + "','" + chklvege_content + "','" + chblvege_content + "','" + pickyfood + "','" + chklvegetime_content + "','" + favorbreak + "','" + favorlunch + "','" + favordinner + "','" + forbid + "','" + specorder + "','" + mealbreak + "','" + mealsnack + "','" + meallunch + "','" + mealtea + "','" + mealdinner + "','" + mealmnsnack + "','" + createuser + "','" + createdate + "','" + creatTime_c + "','Y')";
            SqlCommand insert = new SqlCommand(insertipmeal, conn);
            insert.ExecuteNonQuery();
            warning = "新增成功";
            conn.Close();
        }

        //找住民資訊-基本資枓
        public static void SearchIPInfomation(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string searchipinfo = "SELECT IP_NO, IP_NO_NEW, IP_NAME, SEX, IP_ID, DOB, IP_CNRY, ARC, PASSPORT, RESIDE_DATE, HEIGHT, WEIGHT, IP_PERM_ADR, NOW_ADR, DNR, STATUES, STATUES_EX, BARRIER, ENTER_METHOD, BELIEF, BELIEF_EX, EDUCATION, CAREER, CAREER_EX, LANGUAGE, LANGUAGE2, LANGUAGE3, LANGUAGE_EX, LANGUAGE_EX2, LANGUAGE_EX3, TALK_ABILITY, TALK_ABILITY_EX, ECO_SOURCE, ECO_SOURCE_EX, INSURANCE, INSURANCE_EX, FMLY_PROBLEM, STAT_INFC_HX, STAT_INFC_HX_EX, AFTER_ADMS_HX, AFTER_ADMS_HX_EX, PHOTO, IP_IDENTITY_NO FROM IP_INFORMATION WHERE IP_NO = '" + ipno + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(searchipinfo, conn);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                ip_no = searchlist.GetValue(0).ToString();
                ip_no_new = searchlist.GetValue(1).ToString();
                ip_name = searchlist.GetValue(2).ToString();
                ip_sex = searchlist.GetValue(3).ToString();
                ip_id = searchlist.GetValue(4).ToString();
                ip_birthday = searchlist.GetValue(5).ToString();
                ip_country = searchlist.GetValue(6).ToString();
                ip_arc = searchlist.GetValue(7).ToString();
                ip_passport = searchlist.GetValue(8).ToString();
                ip_residedate = searchlist.GetValue(9).ToString();
                ip_height = searchlist.GetValue(10).ToString();
                ip_weight = searchlist.GetValue(11).ToString();
                ip_permadr = searchlist.GetValue(12).ToString();
                ip_nowadr = searchlist.GetValue(13).ToString();
                ip_dnr = searchlist.GetValue(14).ToString();
                ip_statues = searchlist.GetValue(15).ToString();
                ip_statues_ex = searchlist.GetValue(16).ToString();
                ip_barrier = searchlist.GetValue(17).ToString();
                check_method = searchlist.GetValue(18).ToString();
                ip_belief = searchlist.GetValue(19).ToString();
                ip_belief_ex = searchlist.GetValue(20).ToString();
                ip_education = searchlist.GetValue(21).ToString();
                ip_career = searchlist.GetValue(22).ToString();//CAREER, 
                ip_career_ex = searchlist.GetValue(23).ToString();//CAREER_EX
                ip_language = searchlist.GetValue(24).ToString();//LANGUAGE, 
                ip_language2 = searchlist.GetValue(25).ToString();//LANGUAGE2, 
                ip_language3 = searchlist.GetValue(26).ToString();//LANGUAGE3, 
                ip_language_ex = searchlist.GetValue(27).ToString();//LANGUAGE_EX, 
                ip_language_ex2 = searchlist.GetValue(28).ToString();//LANGUAGE_EX2, 
                ip_language_ex3 = searchlist.GetValue(29).ToString();//LANGUAGE_EX3
                talk_ability = searchlist.GetValue(30).ToString();//TALK_ABILITY, 
                talk_ability_ex = searchlist.GetValue(31).ToString();//TALK_ABILITY_EX, 
                ip_money = searchlist.GetValue(32).ToString();//ECO_SOURCE, 
                ip_money_ex = searchlist.GetValue(33).ToString();//ECO_SOURCE_EX, 
                ip_insurance = searchlist.GetValue(34).ToString();//INSURANCE, 
                ip_insurance_ex = searchlist.GetValue(35).ToString();//INSURANCE_EX,
                family_problem = searchlist.GetValue(36).ToString();//FMLY_PROBLEM, 
                stat_infc_hx = searchlist.GetValue(37).ToString();//STAT_INFC_HX, 
                statINFC = searchlist.GetValue(38).ToString();//STAT_INFC_HX_EX, 
                after_adms_hx = searchlist.GetValue(39).ToString();//AFTER_ADMS_HX, 
                afterADMS = searchlist.GetValue(40).ToString();//AFTER_ADMS_HX_EX, 
                ip_photo = searchlist.GetValue(41).ToString();//PHOTO
                ip_identity = searchlist.GetValue(42).ToString();
            }

            conn.Close();
        }

        //找住民資訊-住民家庭資枓
        public static void SearchIPFamily(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string searchipfamily = "SELECT MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER, IN_REASON, IN_REASON_EX, TRANSFER_UNT, TRANSFER_EX, SIGN_NAME, SIGN_RELATE, SIGN_ADD, SIGN_TEL, SIGN_TEL2, SIGN_TEL3, INVO_NAME, INVO_RELATE, INVO_ADD, INVO_TEL, INVO_TEL2, INVO_TEL3, EMR_NAME, EMR_RELATE, EMR_ADD, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR1_NAME, EMR1_RELATE, EMR1_ADD, EMR1_TEL, EMR1_TEL2, EMR1_TEL3, PAY_NAME, PAY_TEL, PAY_EVALUTE, TRANSFER_NAME,EMR2_NAME,EMR2_ADD,EMR2_TEL,EMR2_TEL2,EMR2_TEL3,EMR2_RELATE, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID FROM IP_FAMILY WHERE IP_NO = '" + ipno + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(searchipfamily, conn);
            try
            {
                SqlDataReader searchlist = search.ExecuteReader();

                if (searchlist.Read())
                {
                    ip_marry = searchlist.GetValue(0).ToString();//MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER
                    ip_marry_ex = searchlist.GetValue(1).ToString();
                    mate_name = searchlist.GetValue(2).ToString();
                    mate_duty = searchlist.GetValue(3).ToString();
                    ip_son = searchlist.GetValue(4).ToString();
                    ip_daughter = searchlist.GetValue(5).ToString();

                    in_reason = searchlist.GetValue(6).ToString();//IN_REASON, 
                    in_reason_ex = searchlist.GetValue(7).ToString();//IN_REASON_EX, 
                    ip_transfer = searchlist.GetValue(8).ToString();//TRANSFER_UNT, 
                    ip_transfer_ex = searchlist.GetValue(9).ToString();//TRANSFER_EX,

                    sign_name = searchlist.GetValue(10).ToString();//SIGN_NAME, 
                    sign_relate = searchlist.GetValue(11).ToString();//SIGN_RELATE,
                    sign_adr = searchlist.GetValue(12).ToString();//SIGN_ADD, 
                    sign_tel = searchlist.GetValue(13).ToString();//SIGN_TEL, 
                    sign_tel2 = searchlist.GetValue(14).ToString();//SIGN_TEL2, 
                    sign_tel3 = searchlist.GetValue(15).ToString();//SIGN_TEL3

                    re_name = searchlist.GetValue(16).ToString();//INVO_NAME, 
                    re_relate = searchlist.GetValue(17).ToString();//INVO_RELATE, 
                    re_adr = searchlist.GetValue(18).ToString();//INVO_ADD, 
                    re_tel = searchlist.GetValue(19).ToString();//INVO_TEL, 
                    re_tel2 = searchlist.GetValue(20).ToString();//INVO_TEL2
                    re_tel3 = searchlist.GetValue(21).ToString();//INVO_TEL3

                    hurry_name = searchlist.GetValue(22).ToString();//EMR_NAME, 
                    hurry_relate = searchlist.GetValue(23).ToString();//EMR_RELATE, 
                    hurry_add = searchlist.GetValue(24).ToString();//EMR_ADD, 
                    hurry_tel = searchlist.GetValue(25).ToString();//EMR_TEL, 
                    hurry_tel2 = searchlist.GetValue(26).ToString();//EMR_TEL2, 
                    hurry_tel3 = searchlist.GetValue(27).ToString();//EMR_TEL3

                    emr_name = searchlist.GetValue(28).ToString();//EMR1_NAME, 
                    emr_relate = searchlist.GetValue(29).ToString();//EMR1_RELATE,
                    emr_add = searchlist.GetValue(30).ToString(); //EMR1_ADD,
                    emr_tel = searchlist.GetValue(31).ToString();//EMR1_TEL, 
                    emr_tel2 = searchlist.GetValue(32).ToString();//EMR1_TEL2, 
                    emr_tel3 = searchlist.GetValue(33).ToString();//EMR1_TEL3

                    emr2_name = searchlist["EMR2_NAME"].ToString();
                    emr2_add = searchlist["EMR2_ADD"].ToString();
                    emr2_tel = searchlist["EMR2_TEL"].ToString();
                    emr2_tel2 = searchlist["EMR2_TEL2"].ToString();
                    emr2_tel3 = searchlist["EMR2_TEL3"].ToString();
                    emr2_relate = searchlist["EMR2_RELATE"].ToString();

                    //PAY_NAME, PAY_TEL, PAY_EVALUTE, TRANSFER_NAME
                    pay_name = searchlist["PAY_NAME"].ToString(); //searchlist.GetValue(34).ToString();
                    pay_tel = searchlist["PAY_TEL"].ToString(); //searchlist.GetValue(35).ToString();
                    pay_evalute = searchlist["PAY_EVALUTE"].ToString(); //searchlist.GetValue(36).ToString();
                    transfer_name = searchlist["TRANSFER_NAME"].ToString(); //searchlist.GetValue(37).ToString();

                    //SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
                    semail = searchlist["SIGN_EMAIL"].ToString();//searchlist.GetValue(38).ToString();
                    remail = searchlist["INVO1_EMAIL"].ToString(); //searchlist.GetValue(39).ToString();
                    hurryemail = searchlist["EMR_EMAIL"].ToString(); //searchlist.GetValue(40).ToString();
                    hurryemail1 = searchlist["EMR1_EMAIL"].ToString(); //searchlist.GetValue(41).ToString();
                    hurryemail2 = searchlist["EMR2_EMAIL"].ToString(); //searchlist.GetValue(42).ToString();
                    payemail1 = searchlist["PAY_EMAIL"].ToString(); //searchlist.GetValue(43).ToString();
                    scutalkid = searchlist["SIGN_CUTALK_MID"].ToString();//searchlist.GetValue(44).ToString();
                    rcutalkid = searchlist["INVO1_CUTALK_MID"].ToString(); //searchlist.GetValue(45).ToString();
                    hurrycutalkid = searchlist["EMR_CUTALK_MID"].ToString(); //searchlist.GetValue(46).ToString();
                    hurrycutalkid1 = searchlist["EMR1_CUTALK_MID"].ToString(); //searchlist.GetValue(47).ToString();
                    hurrycutalkid2 = searchlist["EMR2_CUTALK_MID"].ToString(); //searchlist.GetValue(48).ToString();
                    paycutalkid = searchlist["PAY_CUTALK_MID"].ToString(); //searchlist.GetValue(49).ToString();

                    
                }
            }
            catch 
            { }
            conn.Close();
        }

        //找住民資訊-基本資枓(敬德)
        public static void SearchIPInfomationJ(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string searchipinfo = "SELECT IP_NO, IP_NO_NEW, IP_NAME, SEX, IP_ID, DOB, IP_CNRY, ARC, PASSPORT, RESIDE_DATE, HEIGHT, WEIGHT, IP_PERM_ADR, NOW_ADR, DNR, STATUES, STATUES_EX, BARRIER, ENTER_METHOD, BELIEF, BELIEF_EX, EDUCATION, CAREER, CAREER_EX, LANGUAGE, LANGUAGE2, LANGUAGE3, LANGUAGE_EX, LANGUAGE_EX2, LANGUAGE_EX3, TALK_ABILITY, TALK_ABILITY_EX, ECO_SOURCE, ECO_SOURCE_EX, INSURANCE, INSURANCE_EX, FMLY_PROBLEM, STAT_INFC_HX, STAT_INFC_HX_EX, AFTER_ADMS_HX, AFTER_ADMS_HX_EX, PHOTO, IP_IDENTITY_NO, VISITRESTRICTION FROM IP_INFORMATION WHERE IP_NO = '" + ipno + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(searchipinfo, conn);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                ip_no = searchlist.GetValue(0).ToString();
                ip_no_new = searchlist.GetValue(1).ToString();
                ip_name = searchlist.GetValue(2).ToString();
                ip_sex = searchlist.GetValue(3).ToString();
                ip_id = searchlist.GetValue(4).ToString();
                ip_birthday = searchlist.GetValue(5).ToString();
                ip_country = searchlist.GetValue(6).ToString();
                ip_arc = searchlist.GetValue(7).ToString();
                ip_passport = searchlist.GetValue(8).ToString();
                ip_residedate = searchlist.GetValue(9).ToString();
                ip_height = searchlist.GetValue(10).ToString();
                ip_weight = searchlist.GetValue(11).ToString();
                ip_permadr = searchlist.GetValue(12).ToString();
                ip_nowadr = searchlist.GetValue(13).ToString();
                ip_dnr = searchlist.GetValue(14).ToString();
                ip_statues = searchlist.GetValue(15).ToString();
                ip_statues_ex = searchlist.GetValue(16).ToString();
                ip_barrier = searchlist.GetValue(17).ToString();
                check_method = searchlist.GetValue(18).ToString();
                ip_belief = searchlist.GetValue(19).ToString();
                ip_belief_ex = searchlist.GetValue(20).ToString();
                ip_education = searchlist.GetValue(21).ToString();
                ip_career = searchlist.GetValue(22).ToString();//CAREER, 
                ip_career_ex = searchlist.GetValue(23).ToString();//CAREER_EX
                ip_language = searchlist.GetValue(24).ToString();//LANGUAGE, 
                ip_language2 = searchlist.GetValue(25).ToString();//LANGUAGE2, 
                ip_language3 = searchlist.GetValue(26).ToString();//LANGUAGE3, 
                ip_language_ex = searchlist.GetValue(27).ToString();//LANGUAGE_EX, 
                ip_language_ex2 = searchlist.GetValue(28).ToString();//LANGUAGE_EX2, 
                ip_language_ex3 = searchlist.GetValue(29).ToString();//LANGUAGE_EX3
                talk_ability = searchlist.GetValue(30).ToString();//TALK_ABILITY, 
                talk_ability_ex = searchlist.GetValue(31).ToString();//TALK_ABILITY_EX, 
                ip_money = searchlist.GetValue(32).ToString();//ECO_SOURCE, 
                ip_money_ex = searchlist.GetValue(33).ToString();//ECO_SOURCE_EX, 
                ip_insurance = searchlist.GetValue(34).ToString();//INSURANCE, 
                ip_insurance_ex = searchlist.GetValue(35).ToString();//INSURANCE_EX,
                family_problem = searchlist.GetValue(36).ToString();//FMLY_PROBLEM, 
                stat_infc_hx = searchlist.GetValue(37).ToString();//STAT_INFC_HX, 
                statINFC = searchlist.GetValue(38).ToString();//STAT_INFC_HX_EX, 
                after_adms_hx = searchlist.GetValue(39).ToString();//AFTER_ADMS_HX, 
                afterADMS = searchlist.GetValue(40).ToString();//AFTER_ADMS_HX_EX, 
                ip_photo = searchlist.GetValue(41).ToString();//PHOTO
                ip_identity = searchlist.GetValue(42).ToString();
                visitlimit = searchlist.GetValue(43).ToString();
            }

            conn.Close();
        }
        
        //找住民資訊-住民家庭資枓(敬德)
        public static void SearchIPFamilyJ(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string searchipfamily = "SELECT MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER, IN_REASON, IN_REASON_EX, TRANSFER_UNT, TRANSFER_EX, SIGN_NAME, SIGN_RELATE, SIGN_ADD, SIGN_TEL, SIGN_TEL2, SIGN_TEL3, INVO_NAME, INVO_RELATE, INVO_ADD, INVO_TEL, INVO_TEL2, INVO_TEL3, EMR_NAME, EMR_RELATE, EMR_ADD, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR1_NAME, EMR1_RELATE, EMR1_ADD, EMR1_TEL, EMR1_TEL2, EMR1_TEL3, PAY_NAME, PAY_TEL, PAY_EVALUTE, TRANSFER_NAME,EMR2_NAME,EMR2_ADD,EMR2_TEL,EMR2_TEL2,EMR2_TEL3,EMR2_RELATE, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID, SIGN_ID, PAY_RELATE, PAY2_NAME, PAY2_RELATE, PAY2_TEL, PAY3_NAME, PAY3_RELATE, PAY3_TEL FROM IP_FAMILY WHERE IP_NO = '" + ipno + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(searchipfamily, conn);
            try
            {
                SqlDataReader searchlist = search.ExecuteReader();

                if (searchlist.Read())
                {
                    ip_marry = searchlist.GetValue(0).ToString();//MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER
                    ip_marry_ex = searchlist.GetValue(1).ToString();
                    mate_name = searchlist.GetValue(2).ToString();
                    mate_duty = searchlist.GetValue(3).ToString();
                    ip_son = searchlist.GetValue(4).ToString();
                    ip_daughter = searchlist.GetValue(5).ToString();

                    in_reason = searchlist.GetValue(6).ToString();//IN_REASON, 
                    in_reason_ex = searchlist.GetValue(7).ToString();//IN_REASON_EX, 
                    ip_transfer = searchlist.GetValue(8).ToString();//TRANSFER_UNT, 
                    ip_transfer_ex = searchlist.GetValue(9).ToString();//TRANSFER_EX,

                    sign_name = searchlist.GetValue(10).ToString();//SIGN_NAME,
                    sign_relate = searchlist.GetValue(11).ToString();//SIGN_RELATE,
                    sign_adr = searchlist.GetValue(12).ToString();//SIGN_ADD, 
                    sign_tel = searchlist.GetValue(13).ToString();//SIGN_TEL, 
                    sign_tel2 = searchlist.GetValue(14).ToString();//SIGN_TEL2, 
                    sign_tel3 = searchlist.GetValue(15).ToString();//SIGN_TEL3

                    re_name = searchlist.GetValue(16).ToString();//INVO_NAME, 
                    re_relate = searchlist.GetValue(17).ToString();//INVO_RELATE, 
                    re_adr = searchlist.GetValue(18).ToString();//INVO_ADD, 
                    re_tel = searchlist.GetValue(19).ToString();//INVO_TEL, 
                    re_tel2 = searchlist.GetValue(20).ToString();//INVO_TEL2
                    re_tel3 = searchlist.GetValue(21).ToString();//INVO_TEL3

                    hurry_name = searchlist.GetValue(22).ToString();//EMR_NAME, 
                    hurry_relate = searchlist.GetValue(23).ToString();//EMR_RELATE, 
                    hurry_add = searchlist.GetValue(24).ToString();//EMR_ADD, 
                    hurry_tel = searchlist.GetValue(25).ToString();//EMR_TEL, 
                    hurry_tel2 = searchlist.GetValue(26).ToString();//EMR_TEL2, 
                    hurry_tel3 = searchlist.GetValue(27).ToString();//EMR_TEL3

                    emr_name = searchlist.GetValue(28).ToString();//EMR1_NAME, 
                    emr_relate = searchlist.GetValue(29).ToString();//EMR1_RELATE,
                    emr_add = searchlist.GetValue(30).ToString(); //EMR1_ADD,
                    emr_tel = searchlist.GetValue(31).ToString();//EMR1_TEL, 
                    emr_tel2 = searchlist.GetValue(32).ToString();//EMR1_TEL2, 
                    emr_tel3 = searchlist.GetValue(33).ToString();//EMR1_TEL3

                    emr2_name = searchlist["EMR2_NAME"].ToString();
                    emr2_add = searchlist["EMR2_ADD"].ToString();
                    emr2_tel = searchlist["EMR2_TEL"].ToString();
                    emr2_tel2 = searchlist["EMR2_TEL2"].ToString();
                    emr2_tel3 = searchlist["EMR2_TEL3"].ToString();
                    emr2_relate = searchlist["EMR2_RELATE"].ToString();

                    //PAY_NAME, PAY_TEL, PAY_EVALUTE, TRANSFER_NAME
                    pay_name = searchlist["PAY_NAME"].ToString(); //searchlist.GetValue(34).ToString();
                    pay_tel = searchlist["PAY_TEL"].ToString(); //searchlist.GetValue(35).ToString();
                    pay_evalute = searchlist["PAY_EVALUTE"].ToString(); //searchlist.GetValue(36).ToString();
                    transfer_name = searchlist["TRANSFER_NAME"].ToString(); //searchlist.GetValue(37).ToString();

                    //SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
                    semail = searchlist["SIGN_EMAIL"].ToString();//searchlist.GetValue(38).ToString();
                    remail = searchlist["INVO1_EMAIL"].ToString(); //searchlist.GetValue(39).ToString();
                    hurryemail = searchlist["EMR_EMAIL"].ToString(); //searchlist.GetValue(40).ToString();
                    hurryemail1 = searchlist["EMR1_EMAIL"].ToString(); //searchlist.GetValue(41).ToString();
                    hurryemail2 = searchlist["EMR2_EMAIL"].ToString(); //searchlist.GetValue(42).ToString();
                    payemail1 = searchlist["PAY_EMAIL"].ToString(); //searchlist.GetValue(43).ToString();
                    scutalkid = searchlist["SIGN_CUTALK_MID"].ToString();//searchlist.GetValue(44).ToString();
                    rcutalkid = searchlist["INVO1_CUTALK_MID"].ToString(); //searchlist.GetValue(45).ToString();
                    hurrycutalkid = searchlist["EMR_CUTALK_MID"].ToString(); //searchlist.GetValue(46).ToString();
                    hurrycutalkid1 = searchlist["EMR1_CUTALK_MID"].ToString(); //searchlist.GetValue(47).ToString();
                    hurrycutalkid2 = searchlist["EMR2_CUTALK_MID"].ToString(); //searchlist.GetValue(48).ToString();
                    paycutalkid = searchlist["PAY_CUTALK_MID"].ToString(); //searchlist.GetValue(49).ToString();

                    //敬德要求新增的欄位
                    //SIGN_ID, PAY_RELATE, PAY2_NAME, PAY2_RELATE, PAY2_TEL, PAY3_NAME, PAY3_RELATE, PAY3_TEL
                    //signid, payrelate, pay2name, pay2relate, pay2tel, pay3name, pay3relate, pay3tel
                    signid = searchlist["SIGN_ID"].ToString();
                    payrelate = searchlist["PAY_RELATE"].ToString();
                    pay2name = searchlist["PAY2_NAME"].ToString();
                    pay2relate = searchlist["PAY2_RELATE"].ToString();
                    pay2tel = searchlist["PAY2_TEL"].ToString();
                    pay3name = searchlist["PAY3_NAME"].ToString();
                    pay3relate = searchlist["PAY3_RELAT"].ToString();
                    pay3tel = searchlist["PAY3_TEL"].ToString();


                }
            }
            catch
            { }
            conn.Close();
        }
        
        //找住民資訊-膳食習慣
        public static string SearchIPMealfavor(string ipno)
        {
            string re = "N";
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string searchipmeal = "SELECT * FROM IP_MEALFAVOR WHERE IP_NO = '" + ipno + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(searchipmeal, conn);
            try
            {
                SqlDataReader searchlist = search.ExecuteReader();

                if (searchlist.Read())
                {
                    chklvege_content = searchlist["CARNIVORE1"].ToString();
                    chklvege1_content = searchlist["CARNIVORE1"].ToString();
                    chblvege_content = searchlist["CARNIVORE2"].ToString();
                    pickyfood = searchlist["PICKYFOOD"].ToString();
                    chklvegetime_content = searchlist["VEGETABLE"].ToString();
                    favorbreak = searchlist["BREAKFASTMAINFOOD"].ToString();
                    favorlunch = searchlist["LUNCHMAINFOOD"].ToString();
                    favordinner = searchlist["DINNERMAINFOOD"].ToString();
                    forbid = searchlist["AVOID"].ToString();
                    specorder = searchlist["INSTRUCT"].ToString();
                    mealbreak = searchlist["BREAKFASTREQ"].ToString();
                    mealsnack = searchlist["SNACKREQ"].ToString();
                    meallunch = searchlist["LUNCHREQ"].ToString();
                    mealtea = searchlist["TEABREAKREQ"].ToString();
                    mealdinner = searchlist["DINNERREQ"].ToString();
                    mealmnsnack = searchlist["MNSNACKREQ"].ToString();
                    re = "Y";                   
                }
            }
            catch
            { }
            conn.Close();
            return re;
        }

        

        //尋找此住民的床號
        internal static DataTable SearchIPBadRoom(string ip_no, string connection_id)
        {
            string datasource = connect1(connection_id);
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = datasource;
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NAME, ROOM.ROOM_BED, IP_INFORMATION.IP_NO_NEW FROM IP_INFORMATION INNER JOIN ROOM ON IP_INFORMATION.IP_NO = ROOM.IP_NO WHERE IP_INFORMATION.IP_NO = '" + ip_no + "'";
            // SqlDataReader searchlist = search.ExecuteReader();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            cmd.Dispose();
            con.Close();
            return dt;
        }
        
        public static string IDsearch(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string rthing = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string firstbed = "SELECT IP_NO_NEW FROM IP_INFORMATION WHERE IP_NO = '" + ipno + "' AND AccountEnable = 'Y'";
            SqlCommand first = new SqlCommand(firstbed, conn);
            SqlDataReader yesfirst = first.ExecuteReader();
            if (yesfirst.Read())
            {
                rthing = yesfirst.GetValue(0).ToString();
            }
            else
            {
                rthing = "";
            }
            conn.Close();
            return rthing;
        }

        //修改住民基本資枓
        public static void UpdateIPInformation(string ipno, string ipname, string sex, string ipid, string birthday, string country, string arc, string passport, string residedate, string height, string weight, string permadr, string nowadr, string dnr, string statues, string statuesex, string barrier, string checkmethod, string career, string careerex, string belief, string beliefex, string education, string ipMainlanguage, string ipMainlanguageex, string ipSeclanguage, string ipSeclanguageex, string ipThirdlanguage, string ipThirdlanguageex, string talkability, string talkabilityex, string money, string moneyex, string insurance, string insuranceex, string familyproblem, string statinfchx, string statinfchx_ex, string afteradmshx, string afteradmshx_ex, string ipphoto, string marry, string marryex, string matename, string mateduty, string son, string daughter, string inreason, string inreasonex, string transfer, string transferex, string signname, string signrelate, string signadr, string signtel, string signtel2, string signtel3, string rename, string rerelate, string readr, string retel, string retel2, string retel3, string hurryname, string erelate, string hurrytel, string hurrytel_2, string hurrytel_3, string hurryname1, string erelate1, string hurrytel1, string hurrytel1_2, string hurrytel1_3, string payname, string paytel, string payevalute, string transfername, string opuser, string opdate, string opTime, string emr1_adr, string emr2_adr, string ip_identity_no, string hurryname2, string hurryte2, string hurryte2_2, string hurryte2_3, string erelate2, string emr3_adr, string chklvege_content, string chblvege_content, string chklvegetime_content, string favorbreak, string favorlunch, string favordinner, string forbid, string specorder, string mealbreak, string mealsnack, string meallunch, string mealtea, string mealdinner, string mealmnsnack, string semail, string scutalkid, string remail, string rcutalkid, string hurryemail, string hurrycutalkid, string hurryemail1, string hurrycutalkid1, string hurryemail2, string hurrycutalkid2, string payemail1, string paycutalkid, string pickyfood)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string updateipinfomation = "UPDATE IP_INFORMATION SET IP_NAME = N'" + ipname + "', SEX = '" + sex + "', IP_ID = '" + ipid + "', DOB = '" + birthday + "', IP_CNRY = '" + country + "', ARC = '" + arc + "', PASSPORT = '" + passport + "', RESIDE_DATE = '" + residedate + "', HEIGHT = '" + height + "', WEIGHT = '" + weight + "', IP_PERM_ADR = '" + permadr + "', NOW_ADR = '" + nowadr + "', DNR = '" + dnr + "', STATUES = '" + statues + "', STATUES_EX = '" + statuesex + "', BARRIER = '" + barrier + "',ENTER_METHOD = '" + checkmethod + "', CAREER = '" + career + "', CAREER_EX = '" + careerex + "', BELIEF = '" + belief + "', BELIEF_EX = '" + beliefex + "', EDUCATION = '" + education + "', LANGUAGE = '" + ipMainlanguage + "', LANGUAGE_EX = '" + ipMainlanguageex + "', LANGUAGE2 = '" + ipSeclanguage + "', LANGUAGE_EX2 = '" + ipSeclanguageex + "', LANGUAGE3 = '" + ipThirdlanguage + "', LANGUAGE_EX3 = '" + ipThirdlanguageex + "', TALK_ABILITY = '" + talkability + "', TALK_ABILITY_EX = '" + talkabilityex + "', ECO_SOURCE = '" + money + "', ECO_SOURCE_EX = '" + moneyex + "', INSURANCE = '" + insurance + "', INSURANCE_EX = '" + insuranceex + "', FMLY_PROBLEM = '" + familyproblem + "', STAT_INFC_HX = '" + statinfchx + "', STAT_INFC_HX_EX = '" + statinfchx_ex + "', AFTER_ADMS_HX = '" + afteradmshx + "', AFTER_ADMS_HX_EX = '" + afteradmshx_ex + "', PHOTO = '" + ipphoto + "', IP_IDENTITY_NO = '" + ip_identity_no + "', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "', OP_TIME = '" + opTime + "'  WHERE IP_NO = '" + ipno + "'";
            SqlCommand updateinformation = new SqlCommand(updateipinfomation, conn);
            updateinformation.ExecuteScalar();
            conn.Close();

            conn.Open();
            string updateipfamily = "UPDATE IP_FAMILY SET MARRY = '" + marry + "', MARRY_EX = '" + marryex + "', MATE_NAME = N'" + matename + "', MATE_DUTY = '" + mateduty + "', SON = '" + son + "', DAUGHTER = '" + daughter + "', IN_REASON = '" + inreason + "', IN_REASON_EX = '" + inreasonex + "', TRANSFER_UNT = '" + transfer + "', TRANSFER_EX = '" + transferex + "', SIGN_NAME = N'" + signname + "', SIGN_RELATE = '" + signrelate + "', SIGN_ADD = '" + signadr + "', SIGN_TEL = '" + signtel + "', SIGN_TEL2 = '" + signtel2 + "', SIGN_TEL3 = '" + signtel3 + "', INVO_NAME = N'" + rename + "', INVO_RELATE = '" + rerelate + "',INVO_ADD = '" + readr + "', INVO_TEL = '" + retel + "', INVO_TEL2 = '" + retel2 + "', INVO_TEL3 = '" + retel3 + "', EMR_NAME = N'" + hurryname + "', EMR_RELATE = '" + erelate + "', EMR_TEL = '" + hurrytel + "', EMR_TEL2 = '" + hurrytel_2 + "', EMR_TEL3 = '" + hurrytel_3 + "', EMR1_NAME = N'" + hurryname1 + "', EMR1_RELATE = '" + erelate1 + "', EMR1_TEL = '" + hurrytel1 + "', EMR1_TEL2 = '" + hurrytel1_2 + "', EMR1_TEL3 = '" + hurrytel1_3 + "', PAY_NAME = N'" + payname + "', PAY_TEL = '" + paytel + "', PAY_EVALUTE = '" + payevalute + "', TRANSFER_NAME = N'" + transfername + "', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "', OP_TIME = '" + opTime + "', EMR_ADD = '" + emr1_adr + "', EMR1_ADD = '" + emr2_adr + "', EMR2_NAME = '" + hurryname2 + "', EMR2_ADD = '" + emr3_adr + "', EMR2_TEL = '" + hurryte2 + "', EMR2_TEL2 = '" + hurryte2_2 + "', EMR2_TEL3 = '" + hurryte2_3 + "', EMR2_RELATE = '" + erelate2 + "', SIGN_EMAIL = '" + semail + "', INVO1_EMAIL = '" + remail + "', EMR_EMAIL = '" + hurryemail + "', EMR1_EMAIL = '" + hurryemail1 + "', EMR2_EMAIL = '" + hurryemail2 + "', PAY_EMAIL = '" + payemail1 + "', SIGN_CUTALK_MID = '" + scutalkid + "', INVO1_CUTALK_MID = '" + rcutalkid + "', EMR_CUTALK_MID = '" + hurrycutalkid + "', EMR1_CUTALK_MID = '" + hurrycutalkid1 + "', EMR2_CUTALK_MID = '" + hurrycutalkid2 + "', PAY_CUTALK_MID = '" + paycutalkid + "' WHERE IP_NO = '" + ipno + "'";
            //, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
            //  semail, remail, hurryemail, hurryemail1, hurryemail2, payemail1, scutalkid, rcutalkid, hurrycutalkid, hurrycutalkid1, hurrycutalkid2, paycutalkid
            SqlCommand updatefamily = new SqlCommand(updateipfamily, conn);
            updatefamily.ExecuteScalar();
            conn.Close();

            conn.Open();//
            string opTime_c = opTime.Substring(0, 5).Replace(":", "");
            string updateipmealfavor = "";
            if (SearchIPMealfavor(ipno) == "N")
                updateipmealfavor = "INSERT INTO IP_MEALFAVOR(IP_NO, CARNIVORE1, CARNIVORE2, PICKYFOOD, VEGETABLE, BREAKFASTMAINFOOD, LUNCHMAINFOOD, DINNERMAINFOOD, AVOID, INSTRUCT, BREAKFASTREQ, SNACKREQ, LUNCHREQ, TEABREAKREQ, DINNERREQ, MNSNACKREQ, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable) VALUES('" + ipno + "','" + chklvege_content + "','" + chblvege_content + "','" + pickyfood + "','" + chklvegetime_content + "','" + favorbreak + "','" + favorlunch + "','" + favordinner + "','" + forbid + "','" + specorder + "','" + mealbreak + "','" + mealsnack + "','" + meallunch + "','" + mealtea + "','" + mealdinner + "','" + mealmnsnack + "','" + opuser + "','" + opdate + "','" + opTime_c + "','Y')";
            else
                updateipmealfavor = "UPDATE IP_MEALFAVOR SET CARNIVORE1 = '" + chklvege_content + "', CARNIVORE2 = '" + chblvege_content + "', PICKYFOOD = '" + pickyfood + "', VEGETABLE='" + chklvegetime_content + "', BREAKFASTMAINFOOD = '" + favorbreak + "', LUNCHMAINFOOD = '" + favorlunch + "', DINNERMAINFOOD = '" + favordinner + "', AVOID = '" + forbid + "', INSTRUCT = '" + specorder + "', BREAKFASTREQ = '" + mealbreak + "', SNACKREQ = '" + mealsnack + "', LUNCHREQ ='" + meallunch + "', TEABREAKREQ = '" + mealtea + "', DINNERREQ = '" + mealdinner + "', MNSNACKREQ = '" + mealmnsnack + "', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "', OP_TIME = '" + opTime_c + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updatemealfavor = new SqlCommand(updateipmealfavor, conn);
            updatemealfavor.ExecuteScalar();
            conn.Close();

            warning = "修改成功！";
        }
        //修改住民基本資枓(敬德)
        public static void UpdateIPInformation(string ipno, string ipname, string sex, string ipid, string birthday, string country, string arc, string passport, string residedate, string height, string weight, string permadr, string nowadr, string dnr, string statues, string statuesex, string barrier, string checkmethod, string career, string careerex, string belief, string beliefex, string education, string ipMainlanguage, string ipMainlanguageex, string ipSeclanguage, string ipSeclanguageex, string ipThirdlanguage, string ipThirdlanguageex, string talkability, string talkabilityex, string money, string moneyex, string insurance, string insuranceex, string familyproblem, string statinfchx, string statinfchx_ex, string afteradmshx, string afteradmshx_ex, string ipphoto, string marry, string marryex, string matename, string mateduty, string son, string daughter, string inreason, string inreasonex, string transfer, string transferex, string signname, string signrelate, string signadr, string signtel, string signtel2, string signtel3, string rename, string rerelate, string readr, string retel, string retel2, string retel3, string hurryname, string erelate, string hurrytel, string hurrytel_2, string hurrytel_3, string hurryname1, string erelate1, string hurrytel1, string hurrytel1_2, string hurrytel1_3, string payname, string paytel, string payevalute, string transfername, string opuser, string opdate, string opTime, string emr1_adr, string emr2_adr, string ip_identity_no, string visitlimit, string hurryname2, string hurryte2, string hurryte2_2, string hurryte2_3, string erelate2, string emr3_adr, string chklvege_content, string chblvege_content, string chklvegetime_content, string favorbreak, string favorlunch, string favordinner, string forbid, string specorder, string mealbreak, string mealsnack, string meallunch, string mealtea, string mealdinner, string mealmnsnack, string semail, string scutalkid, string remail, string rcutalkid, string hurryemail, string hurrycutalkid, string hurryemail1, string hurrycutalkid1, string hurryemail2, string hurrycutalkid2, string payemail1, string paycutalkid, string pickyfood, string signid, string payrelate, string pay2name, string pay2relate, string pay2tel, string pay3name, string pay3relate, string pay3tel)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string updateipinfomation = "UPDATE IP_INFORMATION SET IP_NAME = N'" + ipname + "', SEX = '" + sex + "', IP_ID = '" + ipid + "', DOB = '" + birthday + "', IP_CNRY = '" + country + "', ARC = '" + arc + "', PASSPORT = '" + passport + "', RESIDE_DATE = '" + residedate + "', HEIGHT = '" + height + "', WEIGHT = '" + weight + "', IP_PERM_ADR = '" + permadr + "', NOW_ADR = '" + nowadr + "', DNR = '" + dnr + "', STATUES = '" + statues + "', STATUES_EX = '" + statuesex + "', BARRIER = '" + barrier + "',ENTER_METHOD = '" + checkmethod + "', CAREER = '" + career + "', CAREER_EX = '" + careerex + "', BELIEF = '" + belief + "', BELIEF_EX = '" + beliefex + "', EDUCATION = '" + education + "', LANGUAGE = '" + ipMainlanguage + "', LANGUAGE_EX = '" + ipMainlanguageex + "', LANGUAGE2 = '" + ipSeclanguage + "', LANGUAGE_EX2 = '" + ipSeclanguageex + "', LANGUAGE3 = '" + ipThirdlanguage + "', LANGUAGE_EX3 = '" + ipThirdlanguageex + "', TALK_ABILITY = '" + talkability + "', TALK_ABILITY_EX = '" + talkabilityex + "', ECO_SOURCE = '" + money + "', ECO_SOURCE_EX = '" + moneyex + "', INSURANCE = '" + insurance + "', INSURANCE_EX = '" + insuranceex + "', FMLY_PROBLEM = '" + familyproblem + "', STAT_INFC_HX = '" + statinfchx + "', STAT_INFC_HX_EX = '" + statinfchx_ex + "', AFTER_ADMS_HX = '" + afteradmshx + "', AFTER_ADMS_HX_EX = '" + afteradmshx_ex + "', PHOTO = '" + ipphoto + "', IP_IDENTITY_NO = '" + ip_identity_no + "', VISITRESTRICTION = '" + visitlimit + "', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "', OP_TIME = '" + opTime + "'  WHERE IP_NO = '" + ipno + "'";
            SqlCommand updateinformation = new SqlCommand(updateipinfomation, conn);
            updateinformation.ExecuteScalar();
            conn.Close();

            conn.Open();
            string updateipfamily = "UPDATE IP_FAMILY SET MARRY = '" + marry + "', MARRY_EX = '" + marryex + "', MATE_NAME = N'" + matename + "', MATE_DUTY = '" + mateduty + "', SON = '" + son + "', DAUGHTER = '" + daughter + "', IN_REASON = '" + inreason + "', IN_REASON_EX = '" + inreasonex + "', TRANSFER_UNT = '" + transfer + "', TRANSFER_EX = '" + transferex + "', SIGN_NAME = N'" + signname + "', SIGN_RELATE = '" + signrelate + "', SIGN_ADD = '" + signadr + "', SIGN_TEL = '" + signtel + "', SIGN_TEL2 = '" + signtel2 + "', SIGN_TEL3 = '" + signtel3 + "', INVO_NAME = N'" + rename + "', INVO_RELATE = '" + rerelate + "',INVO_ADD = '" + readr + "', INVO_TEL = '" + retel + "', INVO_TEL2 = '" + retel2 + "', INVO_TEL3 = '" + retel3 + "', EMR_NAME = N'" + hurryname + "', EMR_RELATE = '" + erelate + "', EMR_TEL = '" + hurrytel + "', EMR_TEL2 = '" + hurrytel_2 + "', EMR_TEL3 = '" + hurrytel_3 + "', EMR1_NAME = N'" + hurryname1 + "', EMR1_RELATE = '" + erelate1 + "', EMR1_TEL = '" + hurrytel1 + "', EMR1_TEL2 = '" + hurrytel1_2 + "', EMR1_TEL3 = '" + hurrytel1_3 + "', PAY_NAME = N'" + payname + "', PAY_TEL = '" + paytel + "', PAY_EVALUTE = '" + payevalute + "', TRANSFER_NAME = N'" + transfername + "', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "', OP_TIME = '" + opTime + "', EMR_ADD = '" + emr1_adr + "', EMR1_ADD = '" + emr2_adr + "', EMR2_NAME = '" + hurryname2 + "', EMR2_ADD = '" + emr3_adr + "', EMR2_TEL = '" + hurryte2 + "', EMR2_TEL2 = '" + hurryte2_2 + "', EMR2_TEL3 = '" + hurryte2_3 + "', EMR2_RELATE = '" + erelate2 + "', SIGN_EMAIL = '" + semail + "', INVO1_EMAIL = '" + remail + "', EMR_EMAIL = '" + hurryemail + "', EMR1_EMAIL = '" + hurryemail1 + "', EMR2_EMAIL = '" + hurryemail2 + "', PAY_EMAIL = '" + payemail1 + "', SIGN_CUTALK_MID = '" + scutalkid + "', INVO1_CUTALK_MID = '" + rcutalkid + "', EMR_CUTALK_MID = '" + hurrycutalkid + "', EMR1_CUTALK_MID = '" + hurrycutalkid1 + "', EMR2_CUTALK_MID = '" + hurrycutalkid2 + "', PAY_CUTALK_MID = '" + paycutalkid + "', SIGN_ID = '" + signid + "', PAY_RELATE = '" + payrelate + "', PAY2_NAME = '" + pay2name + "', PAY2_RELATE = '" + pay2relate + "', PAY2_TEL = '" + pay2tel + "', PAY3_NAME = '" + pay3name + "', PAY3_RELATE = '" + pay3relate + "', PAY3_TEL = '" + pay3tel + "' WHERE IP_NO = '" + ipno + "'";
            //, SIGN_EMAIL, INVO1_EMAIL, EMR_EMAIL, EMR1_EMAIL, EMR2_EMAIL, PAY_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, EMR_CUTALK_MID, EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
            //  semail, remail, hurryemail, hurryemail1, hurryemail2, payemail1, scutalkid, rcutalkid, hurrycutalkid, hurrycutalkid1, hurrycutalkid2, paycutalkid
            SqlCommand updatefamily = new SqlCommand(updateipfamily, conn);
            updatefamily.ExecuteScalar();
            conn.Close();

            conn.Open();//
            string opTime_c = opTime.Substring(0, 5).Replace(":", "");
            string updateipmealfavor = "";
            if (SearchIPMealfavor(ipno) == "N")
                updateipmealfavor = "INSERT INTO IP_MEALFAVOR(IP_NO, CARNIVORE1, CARNIVORE2, PICKYFOOD, VEGETABLE, BREAKFASTMAINFOOD, LUNCHMAINFOOD, DINNERMAINFOOD, AVOID, INSTRUCT, BREAKFASTREQ, SNACKREQ, LUNCHREQ, TEABREAKREQ, DINNERREQ, MNSNACKREQ, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable) VALUES('" + ipno + "','" + chklvege_content + "','" + chblvege_content + "','" + pickyfood + "','" + chklvegetime_content + "','" + favorbreak + "','" + favorlunch + "','" + favordinner + "','" + forbid + "','" + specorder + "','" + mealbreak + "','" + mealsnack + "','" + meallunch + "','" + mealtea + "','" + mealdinner + "','" + mealmnsnack + "','" + opuser + "','" + opdate + "','" + opTime_c + "','Y')";
            else
                updateipmealfavor = "UPDATE IP_MEALFAVOR SET CARNIVORE1 = '" + chklvege_content + "', CARNIVORE2 = '" + chblvege_content + "', PICKYFOOD = '" + pickyfood + "', VEGETABLE='" + chklvegetime_content + "', BREAKFASTMAINFOOD = '" + favorbreak + "', LUNCHMAINFOOD = '" + favorlunch + "', DINNERMAINFOOD = '" + favordinner + "', AVOID = '" + forbid + "', INSTRUCT = '" + specorder + "', BREAKFASTREQ = '" + mealbreak + "', SNACKREQ = '" + mealsnack + "', LUNCHREQ ='" + meallunch + "', TEABREAKREQ = '" + mealtea + "', DINNERREQ = '" + mealdinner + "', MNSNACKREQ = '" + mealmnsnack + "', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "', OP_TIME = '" + opTime_c + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updatemealfavor = new SqlCommand(updateipmealfavor, conn);
            updatemealfavor.ExecuteScalar();
            conn.Close();

            warning = "修改成功！";
        }
        //修改住民基本資枓
        public static void UpdateIPInformation1(string ipno, string ipname, string sex, string ipid, string birthday, string country, string arc, string passport, string residedate, string height, string weight, string permadr, string nowadr, string dnr, string statues, string statuesex, string barrier, string checkmethod, string career, string careerex, string belief, string beliefex, string education, string ipMainlanguage, string ipMainlanguageex, string ipSeclanguage, string ipSeclanguageex, string ipThirdlanguage, string ipThirdlanguageex, string talkability, string talkabilityex, string money, string moneyex, string insurance, string insuranceex, string familyproblem, string statinfchx, string statinfc_ex, string afteradmshx, string afteradm_ex, string ipphoto, string createuser, string createdate, string creatTime, string newno, string strip_identity_no, string marry, string marryex, string matename, string mateduty, string son, string duaghter, string inreason, string inreasonex, string transfer, string transferex, string signname, string signrelate, string signadr, string signtel, string rename, string rerelate, string readr, string retel, string hurryname, string hurrytel, string payname, string paytel, string payevalute, string transfername, string sphone2, string sphone3, string rphone2, string rphone3, string erelate, string hurrytel_2, string hurrytel_3, string hurryname1, string hurrytel1, string hurrytel1_2, string hurrytel1_3, string erelate1, string emr1_add, string emr2_add, string hurryname2, string hurryte2, string hurryte2_2, string hurryte2_3, string erelate2, string emr3_add, string semail, string scutalkid, string remail, string rcutalkid, string hurryemail, string hurrycutalkid, string hurryemail1, string hurrycutalkid1, string hurryemail2, string hurrycutalkid2, string payemail1, string paycutalkid, string chklvege_content, string chblvege_content, string chklvegetime_content, string favorbreak, string favorlunch, string favordinner, string forbid, string specorder, string mealbreak, string mealsnack, string meallunch, string mealtea, string mealdinner, string mealmnsnack, string pickyfood)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            //IP_NO, IP_NAME,                     SEX,               IP_ID,                 DOB,                      IP_CNRY,                     ARC,                 PASSPORT,                      RESIDE_DATE,                        HEIGHT,                    WEIGHT,                    IP_PERM_ADR,                     NOW_ADR,                    DNR,                 STATUES,                     STATUES_EX,                       BARRIER,                     ENTER_METHOD,                         CAREER,                    CAREER_EX,                      BELIEF,                    BELIEF_EX,                      EDUCATION,                       LANGUAGE,                            LANGUAGE_EX,                              LANGUAGE2,                           LANGUAGE_EX2,                             LANGUAGE3,                             LANGUAGE_EX3,                               TALK_ABILITY,                         TALK_ABILITY_EX,                           ECO_SOURCE,                   ECO_SOURCE_EX,                     INSURANCE,                       INSURANCE_EX,                         FMLY_PROBLEM,                           STAT_INFC_HX,                        STAT_INFC_HX_EX,                         AFTER_ADMS_HX,                         AFTER_ADMS_HX_EX,                        PHOTO,                     CREATE_USER,                        CREATE_DATE,                        CREATE_TIME,                       AccountEnable,       IP_NO_NEW,                  IP_IDENTITY_NO
            string updateipinfomation = "UPDATE IP_INFORMATION SET IP_NAME = N'" + ipname + "', SEX= '" + sex + "', IP_ID= '" + ipid + "', DOB = '" + birthday + "', IP_CNRY = '" + country + "', ARC = '" + arc + "', PASSPORT = '" + passport + "', RESIDE_DATE = '" + residedate + "', HEIGHT = '" + height + "', WEIGHT = '" + weight + "', IP_PERM_ADR = '" + permadr + "', NOW_ADR = '" + nowadr + "', DNR = '" + dnr + "', STATUES = '" + statues + "', STATUES_EX = '" + statuesex + "', BARRIER = '" + barrier + "', ENTER_METHOD = '" + checkmethod + "', CAREER = '" + career + "', CAREER_EX = '" + careerex + "', BELIEF = '" + belief + "', BELIEF_EX = '" + beliefex + "', EDUCATION = '" + education + "', LANGUAGE = '" + ipMainlanguage + "', LANGUAGE_EX = '" + ipMainlanguageex + "', LANGUAGE2 = '" + ipSeclanguage + "', LANGUAGE_EX2 = '" + ipSeclanguageex + "', LANGUAGE3 = '" + ipThirdlanguage + "', LANGUAGE_EX3 = '" + ipThirdlanguageex + "', TALK_ABILITY = '" + talkability + "', TALK_ABILITY_EX = '" + talkabilityex + "', ECO_SOURCE = '" + money + "', ECO_SOURCE_EX = '" + moneyex + "', INSURANCE = '" + insurance + "', INSURANCE_EX = '" + insuranceex + "', FMLY_PROBLEM = '" + familyproblem + "', STAT_INFC_HX = '" + statinfchx + "', STAT_INFC_HX_EX = '" + statinfc_ex + "', AFTER_ADMS_HX = '" + afteradmshx + "', AFTER_ADMS_HX_EX= '" + afteradm_ex + "', PHOTO = '" + ipphoto + "', CREATE_USER = '" + createuser + "', CREATE_DATE = '" + createdate + "', CREATE_TIME = '" + creatTime + "', AccountEnable = 'Y', IP_NO_NEW ='" + newno + "', IP_IDENTITY_NO ='" + strip_identity_no + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updateinformation = new SqlCommand(updateipinfomation, conn);
            updateinformation.ExecuteScalar();
            conn.Close();

            conn.Open();
            //MARRY,                   MARRY_EX,                     MATE_NAME,                      MATE_DUTY,                      SON,                 DAUGHTER,                      IN_REASON,                      IN_REASON_EX,                        TRANSFER_UNT,                      TRANSFER_EX,                        SIGN_NAME,                      SIGN_RELATE,                        SIGN_ADD,                     SIGN_TEL,                     INVO_NAME,                    INVO_RELATE,                      INVO_ADD,                   INVO_TEL,                   EMR_NAME,                       EMR_TEL,                      PAY_NAME,                     PAY_TEL,                    PAY_EVALUTE,                        TRANSFER_NAME,                          CREATE_USER,                        CREATE_DATE,                        CREATE_TIME,                       SIGN_TEL2,                     SIGN_TEL3,                     INVO_TEL2,                     INVO_TEL3,                     EMR_RELATE,                    EMR_TEL2,                        EMR_TEL3 ,                            AccountEnable,      EMR1_NAME,                         EMR1_TEL,                       EMR1_TEL2,                         EMR1_TEL3,                         EMR1_RELATE,                      EMR_ADD,                      EMR1_ADD,                      EMR2_NAME,                         EMR2_ADD,                      EMR2_TEL,                      EMR2_TEL2,                        EMR2_TEL3,                        EMR2_RELATE,                      SIGN_EMAIL,                    INVO1_EMAIL,                    EMR_EMAIL,                        EMR1_EMAIL,                         EMR2_EMAIL,                         PAY_EMAIL,                       SIGN_CUTALK_MID,                       INVO1_CUTALK_MID,                       EMR_CUTALK_MID,                           EMR1_CUTALK_MID,                            EMR2_CUTALK_MID,                            PAY_CUTALK_MID
            string updateipfamily = "UPDATE IP_FAMILY SET MARRY = '" + marry + "', MARRY_EX = '" + marryex + "', MATE_NAME = N'" + matename + "', MATE_DUTY = '" + mateduty + "', SON = '" + son + "', DAUGHTER = '" + duaghter + "', IN_REASON = '" + inreason + "', IN_REASON_EX = '" + inreasonex + "', TRANSFER_UNT = '" + transfer + "', TRANSFER_EX = '" + transferex + "', SIGN_NAME = N'" + signname + "', SIGN_RELATE = '" + signrelate + "', SIGN_ADD = '" + signadr + "', SIGN_TEL = '" + signtel + "', INVO_NAME = N'" + rename + "', INVO_RELATE = '" + rerelate + "', INVO_ADD = '" + readr + "', INVO_TEL = '" + retel + "', EMR_NAME = '" + hurryname + "', EMR_TEL = '" + hurrytel + "', PAY_NAME = N'" + payname + "', PAY_TEL = '" + paytel + "', PAY_EVALUTE = '" + payevalute + "', TRANSFER_NAME = N'" + transfername + "', CREATE_USER = '" + createuser + "', CREATE_DATE = '" + createdate + "', CREATE_TIME = '" + creatTime + "', SIGN_TEL2 = '" + sphone2 + "', SIGN_TEL3 = '" + sphone3 + "', INVO_TEL2 = '" + rphone2 + "', INVO_TEL3 = '" + rphone3 + "', EMR_RELATE= '" + erelate + "', EMR_TEL2 = '" + hurrytel_2 + "', EMR_TEL3 = '" + hurrytel_3 + "', AccountEnable ='Y', EMR1_NAME = N'" + hurryname1 + "', EMR1_TEL = '" + hurrytel1 + "', EMR1_TEL2 = '" + hurrytel1_2 + "', EMR1_TEL3 = '" + hurrytel1_3 + "', EMR1_RELATE = '" + erelate1 + "', EMR_ADD = '" + emr1_add + "', EMR1_ADD = '" + emr2_add + "', EMR2_NAME = N'" + hurryname2 + "', EMR2_ADD = '" + emr3_add + "', EMR2_TEL = '" + hurryte2 + "', EMR2_TEL2 = '" + hurryte2_2 + "', EMR2_TEL3 = '" + hurryte2_3 + "', EMR2_RELATE = '" + erelate2 + "', SIGN_EMAIL = '" + semail + "', INVO1_EMAIL = '" + remail + "', EMR_EMAIL = '" + hurryemail + "', EMR1_EMAIL = '" + hurryemail1 + "', EMR2_EMAIL = '" + hurryemail2 + "', PAY_EMAIL = '" + payemail1 + "', SIGN_CUTALK_MID = '" + scutalkid + "', INVO1_CUTALK_MID = '" + rcutalkid + "', EMR_CUTALK_MID = '" + hurrycutalkid + "', EMR1_CUTALK_MID = '" + hurrycutalkid1 + "', EMR2_CUTALK_MID = '" + hurrycutalkid2 + "', PAY_CUTALK_MID = '" + paycutalkid + "' WHERE IP_NO = '" + ipno + "'";
            //hurryname1 + "','" +             hurrytel1 + "','" +              hurrytel1_2 + "','" +              hurrytel1_3 + "','" +                erelate1 + "','" +            emr1_add + "','" +             emr2_add + "',             N'" + hurryname2 + "','" +             emr3_add + "','" +             hurryte2 + "','" +              hurryte2_2 + "','" +              hurryte2_3 + "','" +                erelate2 + "','" +               semail + "','" +                remail + "','" +              hurryemail + "','" +               hurryemail1 + "','" +               hurryemail2 + "','" +              payemail1 + "','" +                    scutalkid + "','" +                     rcutalkid + "','" +                   hurrycutalkid + "','" +                    hurrycutalkid1 + "','" +                    hurrycutalkid2 + "','" +                   paycutalkid
            SqlCommand updatefamily = new SqlCommand(updateipfamily, conn);
            updatefamily.ExecuteScalar();
            conn.Close();

            conn.Open();
            string cTime_c = creatTime.Substring(0, 2) + creatTime.Substring(3, 2);
            string updateipmealfavor = "";
            if (SearchIPMealfavor(ipno) == "N")
                updateipmealfavor = "INSERT INTO IP_MEALFAVOR(IP_NO, CARNIVORE1, CARNIVORE2, PICKYFOOD, VEGETABLE, BREAKFASTMAINFOOD, LUNCHMAINFOOD, DINNERMAINFOOD, AVOID, INSTRUCT, BREAKFASTREQ, SNACKREQ, LUNCHREQ, TEABREAKREQ, DINNERREQ, MNSNACKREQ, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable) VALUES('" + ipno + "','" + chklvege_content + "','" + chblvege_content + "','" + pickyfood + "','" + chklvegetime_content + "','" + favorbreak + "','" + favorlunch + "','" + favordinner + "','" + forbid + "','" + specorder + "','" + mealbreak + "','" + mealsnack + "','" + meallunch + "','" + mealtea + "','" + mealdinner + "','" + mealmnsnack + "','" + createuser + "','" + createdate + "','" + cTime_c + "','Y')";
            else
                updateipmealfavor = "UPDATE IP_MEALFAVOR SET CARNIVORE1 = '" + chklvege_content + "', CARNIVORE2 = '" + chblvege_content + "', PICKYFOOD = '" + pickyfood + "', VEGETABLE='" + chklvegetime_content + "', BREAKFASTMAINFOOD = '" + favorbreak + "', LUNCHMAINFOOD = '" + favorlunch + "', DINNERMAINFOOD = '" + favordinner + "', AVOID = '" + forbid + "', INSTRUCT = '" + specorder + "', BREAKFASTREQ = '" + mealbreak + "', SNACKREQ = '" + mealsnack + "', LUNCHREQ ='" + meallunch + "', TEABREAKREQ = '" + mealtea + "', DINNERREQ = '" + mealdinner + "', MNSNACKREQ = '" + mealmnsnack + "', CREATE_USER = '" + createuser + "', CREATE_DATE = '" + createdate + "', CREATE_TIME = '" + cTime_c + "', AccountEnable ='Y' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updatemealfavor = new SqlCommand(updateipmealfavor, conn);
            updatemealfavor.ExecuteScalar();
            conn.Close();

            warning = "修改成功";
        }
        //修改住民基本資枓(敬德)
        public static void UpdateIPInformation1(string ipno, string ipname, string sex, string ipid, string birthday, string country, string arc, string passport, string residedate, string height, string weight, string permadr, string nowadr, string dnr, string statues, string statuesex, string barrier, string checkmethod, string career, string careerex, string belief, string beliefex, string education, string ipMainlanguage, string ipMainlanguageex, string ipSeclanguage, string ipSeclanguageex, string ipThirdlanguage, string ipThirdlanguageex, string talkability, string talkabilityex, string money, string moneyex, string insurance, string insuranceex, string familyproblem, string statinfchx, string statinfc_ex, string afteradmshx, string afteradm_ex, string ipphoto, string createuser, string createdate, string creatTime, string newno, string strip_identity_no, string visitlimit, string marry, string marryex, string matename, string mateduty, string son, string duaghter, string inreason, string inreasonex, string transfer, string transferex, string signname, string signrelate, string signadr, string signtel, string rename, string rerelate, string readr, string retel, string hurryname, string hurrytel, string payname, string paytel, string payevalute, string transfername, string sphone2, string sphone3, string rphone2, string rphone3, string erelate, string hurrytel_2, string hurrytel_3, string hurryname1, string hurrytel1, string hurrytel1_2, string hurrytel1_3, string erelate1, string emr1_add, string emr2_add, string hurryname2, string hurryte2, string hurryte2_2, string hurryte2_3, string erelate2, string emr3_add, string semail, string scutalkid, string remail, string rcutalkid, string hurryemail, string hurrycutalkid, string hurryemail1, string hurrycutalkid1, string hurryemail2, string hurrycutalkid2, string payemail1, string paycutalkid, string signid, string payrelate, string pay2name, string pay2relate, string pay2tel, string pay3name, string pay3relate, string pay3tel, string chklvege_content, string chblvege_content, string chklvegetime_content, string favorbreak, string favorlunch, string favordinner, string forbid, string specorder, string mealbreak, string mealsnack, string meallunch, string mealtea, string mealdinner, string mealmnsnack, string pickyfood)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            //IP_NO, IP_NAME,                     SEX,               IP_ID,                 DOB,                      IP_CNRY,                     ARC,                 PASSPORT,                      RESIDE_DATE,                        HEIGHT,                    WEIGHT,                    IP_PERM_ADR,                     NOW_ADR,                    DNR,                 STATUES,                     STATUES_EX,                       BARRIER,                     ENTER_METHOD,                         CAREER,                    CAREER_EX,                      BELIEF,                    BELIEF_EX,                      EDUCATION,                       LANGUAGE,                            LANGUAGE_EX,                              LANGUAGE2,                           LANGUAGE_EX2,                             LANGUAGE3,                             LANGUAGE_EX3,                               TALK_ABILITY,                         TALK_ABILITY_EX,                           ECO_SOURCE,                   ECO_SOURCE_EX,                     INSURANCE,                       INSURANCE_EX,                         FMLY_PROBLEM,                           STAT_INFC_HX,                        STAT_INFC_HX_EX,                         AFTER_ADMS_HX,                         AFTER_ADMS_HX_EX,                        PHOTO,                     CREATE_USER,                        CREATE_DATE,                        CREATE_TIME,                       AccountEnable,       IP_NO_NEW,                  IP_IDENTITY_NO
            string updateipinfomation = "UPDATE IP_INFORMATION SET IP_NAME = N'" + ipname + "', SEX= '" + sex + "', IP_ID= '" + ipid + "', DOB = '" + birthday + "', IP_CNRY = '" + country + "', ARC = '" + arc + "', PASSPORT = '" + passport + "', RESIDE_DATE = '" + residedate + "', HEIGHT = '" + height + "', WEIGHT = '" + weight + "', IP_PERM_ADR = '" + permadr + "', NOW_ADR = '" + nowadr + "', DNR = '" + dnr + "', STATUES = '" + statues + "', STATUES_EX = '" + statuesex + "', BARRIER = '" + barrier + "', ENTER_METHOD = '" + checkmethod + "', CAREER = '" + career + "', CAREER_EX = '" + careerex + "', BELIEF = '" + belief + "', BELIEF_EX = '" + beliefex + "', EDUCATION = '" + education + "', LANGUAGE = '" + ipMainlanguage + "', LANGUAGE_EX = '" + ipMainlanguageex + "', LANGUAGE2 = '" + ipSeclanguage + "', LANGUAGE_EX2 = '" + ipSeclanguageex + "', LANGUAGE3 = '" + ipThirdlanguage + "', LANGUAGE_EX3 = '" + ipThirdlanguageex + "', TALK_ABILITY = '" + talkability + "', TALK_ABILITY_EX = '" + talkabilityex + "', ECO_SOURCE = '" + money + "', ECO_SOURCE_EX = '" + moneyex + "', INSURANCE = '" + insurance + "', INSURANCE_EX = '" + insuranceex + "', FMLY_PROBLEM = '" + familyproblem + "', STAT_INFC_HX = '" + statinfchx + "', STAT_INFC_HX_EX = '" + statinfc_ex + "', AFTER_ADMS_HX = '" + afteradmshx + "', AFTER_ADMS_HX_EX= '" + afteradm_ex + "', PHOTO = '" + ipphoto + "', CREATE_USER = '" + createuser + "', CREATE_DATE = '" + createdate + "', CREATE_TIME = '" + creatTime + "', AccountEnable = 'Y', IP_NO_NEW ='" + newno + "', IP_IDENTITY_NO ='" + strip_identity_no + "', VISITRESTRICTION = '" + visitlimit + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updateinformation = new SqlCommand(updateipinfomation, conn);
            updateinformation.ExecuteScalar();
            conn.Close();

            conn.Open();
            //MARRY,                   MARRY_EX,                     MATE_NAME,                      MATE_DUTY,                      SON,                 DAUGHTER,                      IN_REASON,                      IN_REASON_EX,                        TRANSFER_UNT,                      TRANSFER_EX,                        SIGN_NAME,                      SIGN_RELATE,                        SIGN_ADD,                     SIGN_TEL,                     INVO_NAME,                    INVO_RELATE,                      INVO_ADD,                   INVO_TEL,                   EMR_NAME,                       EMR_TEL,                      PAY_NAME,                     PAY_TEL,                    PAY_EVALUTE,                        TRANSFER_NAME,                          CREATE_USER,                        CREATE_DATE,                        CREATE_TIME,                       SIGN_TEL2,                     SIGN_TEL3,                     INVO_TEL2,                     INVO_TEL3,                     EMR_RELATE,                    EMR_TEL2,                        EMR_TEL3 ,                            AccountEnable,      EMR1_NAME,                         EMR1_TEL,                       EMR1_TEL2,                         EMR1_TEL3,                         EMR1_RELATE,                      EMR_ADD,                      EMR1_ADD,                      EMR2_NAME,                         EMR2_ADD,                      EMR2_TEL,                      EMR2_TEL2,                        EMR2_TEL3,                        EMR2_RELATE,                      SIGN_EMAIL,                    INVO1_EMAIL,                    EMR_EMAIL,                        EMR1_EMAIL,                         EMR2_EMAIL,                         PAY_EMAIL,                       SIGN_CUTALK_MID,                       INVO1_CUTALK_MID,                       EMR_CUTALK_MID,                           EMR1_CUTALK_MID,                            EMR2_CUTALK_MID,                            PAY_CUTALK_MID
            string updateipfamily = "UPDATE IP_FAMILY SET MARRY = '" + marry + "', MARRY_EX = '" + marryex + "', MATE_NAME = N'" + matename + "', MATE_DUTY = '" + mateduty + "', SON = '" + son + "', DAUGHTER = '" + duaghter + "', IN_REASON = '" + inreason + "', IN_REASON_EX = '" + inreasonex + "', TRANSFER_UNT = '" + transfer + "', TRANSFER_EX = '" + transferex + "', SIGN_NAME = N'" + signname + "', SIGN_RELATE = '" + signrelate + "', SIGN_ADD = '" + signadr + "', SIGN_TEL = '" + signtel + "', INVO_NAME = N'" + rename + "', INVO_RELATE = '" + rerelate + "', INVO_ADD = '" + readr + "', INVO_TEL = '" + retel + "', EMR_NAME = '" + hurryname + "', EMR_TEL = '" + hurrytel + "', PAY_NAME = N'" + payname + "', PAY_TEL = '" + paytel + "', PAY_EVALUTE = '" + payevalute + "', TRANSFER_NAME = N'" + transfername + "', CREATE_USER = '" + createuser + "', CREATE_DATE = '" + createdate + "', CREATE_TIME = '" + creatTime + "', SIGN_TEL2 = '" + sphone2 + "', SIGN_TEL3 = '" + sphone3 + "', INVO_TEL2 = '" + rphone2 + "', INVO_TEL3 = '" + rphone3 + "', EMR_RELATE= '" + erelate + "', EMR_TEL2 = '" + hurrytel_2 + "', EMR_TEL3 = '" + hurrytel_3 + "', AccountEnable ='Y', EMR1_NAME = N'" + hurryname1 + "', EMR1_TEL = '" + hurrytel1 + "', EMR1_TEL2 = '" + hurrytel1_2 + "', EMR1_TEL3 = '" + hurrytel1_3 + "', EMR1_RELATE = '" + erelate1 + "', EMR_ADD = '" + emr1_add + "', EMR1_ADD = '" + emr2_add + "', EMR2_NAME = N'" + hurryname2 + "', EMR2_ADD = '" + emr3_add + "', EMR2_TEL = '" + hurryte2 + "', EMR2_TEL2 = '" + hurryte2_2 + "', EMR2_TEL3 = '" + hurryte2_3 + "', EMR2_RELATE = '" + erelate2 + "', SIGN_EMAIL = '" + semail + "', INVO1_EMAIL = '" + remail + "', EMR_EMAIL = '" + hurryemail + "', EMR1_EMAIL = '" + hurryemail1 + "', EMR2_EMAIL = '" + hurryemail2 + "', PAY_EMAIL = '" + payemail1 + "', SIGN_CUTALK_MID = '" + scutalkid + "', INVO1_CUTALK_MID = '" + rcutalkid + "', EMR_CUTALK_MID = '" + hurrycutalkid + "', EMR1_CUTALK_MID = '" + hurrycutalkid1 + "', EMR2_CUTALK_MID = '" + hurrycutalkid2 + "', PAY_CUTALK_MID = '" + paycutalkid + "', SIGN_ID = '" + signid + "', PAY_RELATE = '" + payrelate + "', PAY2_NAME = '" + pay2name + "', PAY2_RELATE = '" + pay2relate + "', PAY2_TEL = '" + pay2tel + "', PAY3_NAME = '" + pay3name + "', PAY3_RELATE = '" + pay3relate + "', PAY3_TEL = '" + pay3tel + "' WHERE IP_NO = '" + ipno + "'";
            //hurryname1 + "','" +             hurrytel1 + "','" +              hurrytel1_2 + "','" +              hurrytel1_3 + "','" +                erelate1 + "','" +            emr1_add + "','" +             emr2_add + "',             N'" + hurryname2 + "','" +             emr3_add + "','" +             hurryte2 + "','" +              hurryte2_2 + "','" +              hurryte2_3 + "','" +                erelate2 + "','" +               semail + "','" +                remail + "','" +              hurryemail + "','" +               hurryemail1 + "','" +               hurryemail2 + "','" +              payemail1 + "','" +                    scutalkid + "','" +                     rcutalkid + "','" +                   hurrycutalkid + "','" +                    hurrycutalkid1 + "','" +                    hurrycutalkid2 + "','" +                   paycutalkid
            SqlCommand updatefamily = new SqlCommand(updateipfamily, conn);
            updatefamily.ExecuteScalar();
            conn.Close();

            conn.Open();
            string cTime_c = creatTime.Substring(0, 2) + creatTime.Substring(3, 2);
            string updateipmealfavor = "";
            if (SearchIPMealfavor(ipno) == "N")
                updateipmealfavor = "INSERT INTO IP_MEALFAVOR(IP_NO, CARNIVORE1, CARNIVORE2, PICKYFOOD, VEGETABLE, BREAKFASTMAINFOOD, LUNCHMAINFOOD, DINNERMAINFOOD, AVOID, INSTRUCT, BREAKFASTREQ, SNACKREQ, LUNCHREQ, TEABREAKREQ, DINNERREQ, MNSNACKREQ, CREATE_USER, CREATE_DATE, CREATE_TIME, AccountEnable) VALUES('" + ipno + "','" + chklvege_content + "','" + chblvege_content + "','" + pickyfood + "','" + chklvegetime_content + "','" + favorbreak + "','" + favorlunch + "','" + favordinner + "','" + forbid + "','" + specorder + "','" + mealbreak + "','" + mealsnack + "','" + meallunch + "','" + mealtea + "','" + mealdinner + "','" + mealmnsnack + "','" + createuser + "','" + createdate + "','" + cTime_c + "','Y')";
            else
                updateipmealfavor = "UPDATE IP_MEALFAVOR SET CARNIVORE1 = '" + chklvege_content + "', CARNIVORE2 = '" + chblvege_content + "', PICKYFOOD = '" + pickyfood + "', VEGETABLE='" + chklvegetime_content + "', BREAKFASTMAINFOOD = '" + favorbreak + "', LUNCHMAINFOOD = '" + favorlunch + "', DINNERMAINFOOD = '" + favordinner + "', AVOID = '" + forbid + "', INSTRUCT = '" + specorder + "', BREAKFASTREQ = '" + mealbreak + "', SNACKREQ = '" + mealsnack + "', LUNCHREQ ='" + meallunch + "', TEABREAKREQ = '" + mealtea + "', DINNERREQ = '" + mealdinner + "', MNSNACKREQ = '" + mealmnsnack + "', CREATE_USER = '" + createuser + "', CREATE_DATE = '" + createdate + "', CREATE_TIME = '" + cTime_c + "', AccountEnable ='Y' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updatemealfavor = new SqlCommand(updateipmealfavor, conn);
            updatemealfavor.ExecuteScalar();
            conn.Close();

            warning = "修改成功";
        }
        /*
         SELECT      IP_NO, MARRY, MARRY_EX, MATE_NAME, MATE_DUTY, SON, DAUGHTER, IN_REASON, IN_REASON_EX, TRANSFER_UNT, TRANSFER_EX, SIGN_NAME, 
                      SIGN_RELATE, SIGN_ADD, SIGN_TEL, SIGN_TEL2, SIGN_TEL3, INVO_NAME, INVO_RELATE, INVO_ADD, INVO_TEL, INVO_TEL2, INVO_TEL3, INVO_NAME_2, 
                      INVO_RELATE_2, INVO_ADD_2, INVO_TEL_2, INVO_TEL_22, INVO_TEL_23, INVO_NAME_3, INVO_RELATE_3, INVO_ADD_3, INVO_TEL_3, INVO_TEL_32, INVO_TEL_33,
                       EMR_NAME, EMR_ADD, EMR_TEL, EMR_TEL2, EMR_TEL3, PAY_NAME, PAY_TEL, PAY_TEL2, PAY_TEL3, PAY_EVALUTE, TRANSFER_NAME, CREATE_USER, 
                      CREATE_DATE, CREATE_TIME, OP_USER, OP_DATE, OP_TIME, AccountEnable, EMR_RELATE, EMR1_NAME, EMR1_ADD, EMR1_TEL, EMR1_TEL2, EMR1_TEL3, 
                      EMR1_RELATE, SIGN_EMAIL, INVO1_EMAIL, INVO2_EMAIL, INVO3_EMAIL, EMR_EMAIL, EMR1_EMAIL, PAY_EMAIL, EMR2_NAME, EMR2_ADD, EMR2_TEL, 
                      EMR2_TEL2, EMR2_TEL3, EMR2_RELATE, EMR2_EMAIL, SIGN_CUTALK_MID, INVO1_CUTALK_MID, INVO2_CUTALK_MID, INVO3_CUTALK_MID, EMR_CUTALK_MID, 
                      EMR1_CUTALK_MID, EMR2_CUTALK_MID, PAY_CUTALK_MID
        FROM         IP_FAMILY
        */
        public static string FindIPStatues(string statuesno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string statuesname = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT STATUES_STATE FROM CODE_IP_STATUES WHERE STATUES_ID = '" + statuesno + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                statuesname = getname.GetValue(0).ToString();
            }
            conn.Close();
            return statuesname;
        }
        public static string FindCheckMethod(string checkmothodno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string checkmothodname = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT ENTER_STATE FROM CODE_IP_CHECK_METHOD WHERE ENTER_ID = '" + checkmothodno + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                checkmothodname = getname.GetValue(0).ToString();
            }
            conn.Close();
            return checkmothodname;
        }
        public static string FindIPBelief(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT BELIEF_STATE FROM CODE_IP_BELIEF WHERE BELIEF_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPEducation(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT EDUCATION_STATE FROM CODE_IP_EDUCATION WHERE EDUCATION_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPCareer(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT CAREER_STATE FROM CODE_IP_CAREER WHERE CAREER_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPLanguage(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT LANGUAGE_STATE FROM CODE_IP_LANGUAGE WHERE LANGUAGE_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindTalkAbility(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT TALK_STATE FROM CODE_IP_TALK_ABILITY WHERE TALK_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPMoney(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT ECO_STATE FROM CODE_IP_ECO_SOURCE WHERE ECO_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPInsurance(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT INSURANCE_STATE FROM CODE_IP_INSURANCE WHERE INSURANCE_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindFamilyProblem(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT PROBLEM_STATE FROM CODE_IP_FAMILY_PROBLEM WHERE PROBLEM_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPMarry(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT MARRY_STATE FROM CODE_IP_MARRY WHERE MARRY_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindMateDuty(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT DUTY_STATE FROM CODE_IP_MATE_DUTY WHERE DUTY_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindInReason(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT INREASON_STATE FROM CODE_IP_IN_REASON WHERE INREASON_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }
        public static string FindIPTransfer(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string name = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string findname = "SELECT TRANSFER_STATE FROM CODE_IP_TRANSFER WHERE TRANSFER_ID = '" + no + "'";
            SqlCommand search = new SqlCommand(findname, conn);
            SqlDataReader getname = search.ExecuteReader();
            if (getname.Read())
            {
                name = getname.GetValue(0).ToString();
            }
            conn.Close();
            return name;
        }

        public static string CheckIn(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string checkipin = "SELECT COUNT(IP_NO) FROM ROOM WHERE IP_NO = '" + ipno + "'";
            SqlCommand chechip = new SqlCommand(checkipin, conn);
            string haveip = chechip.ExecuteScalar().ToString();
            conn.Close();
            return haveip;

        }
        public static void DeleteIPInfo(string ipno, string opuser, string opdate)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string deleteipinfo = "UPDATE IP_INFORMATION SET AccountEnable = 'N', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand update = new SqlCommand(deleteipinfo, conn);
            update.ExecuteScalar();
            conn.Close();
            conn.Open();
            string deleteipfamily = "UPDATE IP_FAMILY SET AccountEnable = 'N', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updatefamily = new SqlCommand(deleteipfamily, conn);
            updatefamily.ExecuteScalar();
            conn.Close();
            conn.Open();
            string deleteipmealfavor = "UPDATE IP_MEALFAVOR SET AccountEnable = 'N', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updatemealfavor = new SqlCommand(deleteipmealfavor, conn);
            updatefamily.ExecuteScalar();
            conn.Close();
            conn.Open();
            string deleteipallowance = "UPDATE IP_ALLOWANCE SET AccountEnable = 'N', OP_USER = '" + opuser + "', OP_DATE = '" + opdate + "' WHERE IP_NO = '" + ipno + "'";
            SqlCommand updateallowance = new SqlCommand(deleteipallowance, conn);
            updateallowance.ExecuteScalar();
            conn.Close();
            warning = "刪除成功！";
        }
        public static void DeletePic(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string deleteippic = "UPDATE IP_INFORMATION SET PHOTO = '' WHERE IP_NO = '" + ipno + "'";
            SqlCommand update = new SqlCommand(deleteippic, conn);
            update.ExecuteScalar();
            conn.Close();
        }
        public static void UpdateMaxNo(string ipyear, string ipmaxno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string updatemaxno = "UPDATE IP_MAX_NO SET MAX_NO = '" + ipmaxno + "' WHERE IP_YEAR = '" + ipyear + "'";
            SqlCommand update = new SqlCommand(updatemaxno, conn);
            update.ExecuteScalar();
            conn.Close();
        }

        public static string GetSqlcom(string sql1, string sql2, string sql3, string sql4, string sql5)
        {
            sqlcom = "SELECT I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, CONVERT(VARCHAR(10), CAST(I.DOB AS DATETIME), 111) AS DOB, I.CREATE_USER, ROOM.ROOM_BED FROM IP_INFORMATION as I Left Outer Join ROOM ON I.IP_NO = ROOM.IP_NO WHERE (I.AccountEnable = 'Y') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) " + sql1 + sql2 + sql3 + sql4 + sql5 + " order by I.IP_NO";
            
            return sqlcom;
        }


        public static string SearchLastIPB_RECORD(string num)
        {
            sqlcom = "SELECT top " + num + " I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, CONVERT(VARCHAR(10), CAST(I.DOB AS DATETIME), 111) AS DOB, I.CREATE_USER, ROOM.ROOM_BED FROM IP_INFORMATION as I Left Outer Join ROOM ON I.IP_NO = ROOM.IP_NO WHERE (I.AccountEnable = 'Y') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) order by I.CREATE_DATE DESC, I.CREATE_TIME DESC";

            return sqlcom;
        }

        public static string SearchLastDayIPB_RECORD(string e_date, string s_date)
        {
            sqlcom = "SELECT I.IP_NO, I.IP_NO_NEW, I.IP_NAME, I.IP_ID, CONVERT(VARCHAR(10), CAST(I.DOB AS DATETIME), 111) AS DOB, I.CREATE_USER, ROOM.ROOM_BED FROM IP_INFORMATION as I Left Outer Join ROOM ON I.IP_NO = ROOM.IP_NO WHERE (I.CREATE_DATE >= '" + s_date + "' AND I.CREATE_DATE <= '" + e_date + "') AND (I.AccountEnable = 'Y') AND (ROOM.REPBED != 'N' OR ROOM.REPBED IS NULL) order by I.CREATE_DATE DESC, I.CREATE_TIME DESC";

            return sqlcom;
        }

        public static string GetIPNOInCom(string sql1, string sql2, string sql3, string sql4)
        {
            sqlcom = "SELECT I.IP_NO, I.IP_NAME FROM ROOM R, IP_INFORMATION I WHERE I.AccountEnable = 'Y'" + sql1 + sql2 + sql3 + sql4;
            return sqlcom;
        }
        public static string GetSqlcomNoIn(string sql1, string sql2, string sql3, string sql4)
        {
            sqlcom = "SELECT I.IP_NO, I.IP_NAME,I.IP_ID,I.DOB,I.IP_NO_NEW FROM IP_INFORMATION I WHERE I.AccountEnable = 'Y'" + sql1 + sql2 + sql3 + sql4;
            return sqlcom;
        }

        public static string CheckIPIn(string sql1, string sql2, string sql3, string sql4, string sql5)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            string checkip = "";

            string CheckIP = "SELECT I.IP_NO FROM ROOM R, IP_INFORMATION I WHERE R.IP_NO = I.IP_NO AND I.AccountEnable = 'Y' AND R.ROOM_STATE = '1'" + sql1 + sql2 + sql3 + sql4 + sql5;
            SqlCommand search = new SqlCommand(CheckIP, conn);
            SqlDataReader searchlist = search.ExecuteReader();
            if (searchlist.Read())
            {
                checkip = "1";
            }
            else
                checkip = "0";
            conn.Close();
            return checkip;
        }
        public static string[] GetRelatePharse()
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string[] relate_pharse;
            string RelatePharse = "SELECT PHRASE FROM PHRASE WHERE PHRASE_NAME = '主簽約者關係'";
            SqlCommand search = new SqlCommand(RelatePharse, conn);
            SqlDataReader pharselist = search.ExecuteReader();
            int count = 0;
            while (pharselist.Read())
            {
                count++;
            }
            pharselist.Close();
            relate_pharse = new string[count];
            pharselist = search.ExecuteReader();
            count = 0;
            while (pharselist.Read())
            {
                relate_pharse[count] = pharselist.GetValue(0).ToString().Trim();
                count++;
            }
            pharselist.Close();
            conn.Close();
            return relate_pharse;

        }
        public static void IPPassword(string ipno, string name)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string insertauth = "INSERT INTO EMP_LOGIN(Login, Password, Name, LOGIN_IDENTITY,AccountEnable) VALUES('" + ipno + "','" + ipno + "','" + name + "', 'N', 'Y')";
            SqlCommand insert = new SqlCommand(insertauth, conn);
            insert.ExecuteNonQuery();
            conn.Close();
        }

        public static string gettime()
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            string cmdtime = " select CONVERT(varchar(12) , getdate(), 108 )";
            SqlCommand gettime = new SqlCommand(cmdtime, conn);
            SqlDataReader time = gettime.ExecuteReader();
            string now = "";
            if (time.Read())
            {
                now = time.GetValue(0).ToString();
            }
            time.Close();
            conn.Close();
            return now;
        }
        public static void NSETSELECT2()
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            string cmdtime = "SELECT * FROM CODE_NSETTING";
            SqlCommand getdata = new SqlCommand(cmdtime, conn);
            SqlDataReader data = getdata.ExecuteReader();
            if (data.Read())
            {
                nnselect = data.GetValue(0).ToString();
                nncount = data.GetValue(1).ToString();
                nnfrom = data.GetValue(2).ToString();
            }
            data.Close();
            conn.Close();
        }
        public static string IDselect(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            string rthing = "";
            conn.ConnectionString = datasource;
            conn.Open();

            string firstbed = "SELECT TOP 1 IP_NO FROM IP_INFORMATION WHERE IP_NO_NEW = '" + ipno + "' AND AccountEnable = 'Y' ORDER BY IP_NO DESC";
            SqlCommand first = new SqlCommand(firstbed, conn);
            SqlDataReader yesfirst = first.ExecuteReader();
            if (yesfirst.Read())
            {
                rthing = yesfirst.GetValue(0).ToString();
            }
            else
            {
                rthing = "";
            }
            conn.Close();
            return rthing;
        }
        public static void UpdateNfrom(string maxno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            try
            {
                conn.ConnectionString = datasource;
                conn.Open();
                string cmdupdate = "UPDATE CODE_NSETTING SET NFROM ='" + maxno + "'";
                SqlCommand update = new SqlCommand(cmdupdate, conn);
                update.ExecuteScalar();
                conn.Close();
            }
            catch
            {
            }
            conn.Close();
        }
        public static int NSETSELECT()
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            string cmdC = "SELECT * FROM CODE_NSETTING";
            SqlCommand getName = new SqlCommand(cmdC, conn);
            SqlDataReader Name = getName.ExecuteReader();
            int name = 0;
            if (Name.Read())
            {
                ++name;
            }
            Name.Close();
            conn.Close();
            return name;
        }
        public static string GetIPNO()
        {
            return ip_no;
        }
        public static string GetIPNO2()
        {
            return ip_no_new;
        }
        public static string GetIPName()
        {
            return ip_name;
        }
        public static string GetIPSex()
        {
            return ip_sex;
        }
        public static string GetIPID()
        {
            return ip_id;
        }
        public static string GetIPBirthday()
        {
            return ip_birthday;
        }
        public static string GetIPCountry()
        {
            return ip_country;
        }
        public static string GetIPARC()
        {
            return ip_arc;
        }
        public static string GetIPPassport()
        {
            return ip_passport;
        }
        public static string GetIPReside_Date()
        {
            return ip_residedate;
        }
        public static string GetIPHeight()
        {
            return ip_height;
        }
        public static string GetIPWeight()
        {
            return ip_weight;
        }
        public static string GetIPPermAdr()
        {
            return ip_permadr;
        }
        public static string GetIPNowAdr()
        {
            return ip_nowadr;
        }
        public static string GetDNR()
        {
            return ip_dnr;
        }
        public static string GetIPStatues()
        {
            return ip_statues;
        }
        public static string GetIPStatuesEx()
        {
            return ip_statues_ex;
        }
        public static string GetIPBarrier()
        {
            return ip_barrier;
        }
        public static string GetCheckMethod()
        {
            return check_method;
        }
        public static string GetIPBelief()
        {
            return ip_belief;
        }
        public static string GetIPBeliefEx()
        {
            return ip_belief_ex;
        }
        public static string GetIPEducation()
        {
            return ip_education;
        }
        public static string GetIPCareer()
        {
            return ip_career;
        }
        public static string GetIPCareerEx()
        {
            return ip_career_ex;
        }
        public static string GetIPLanguage()
        {
            return ip_language;
        }
        public static string GetIPLanguageEx()
        {
            return ip_language_ex;
        }
        public static string GetIPLanguage2()
        {
            return ip_language2;
        }
        public static string GetIPLanguageEx2()
        {
            return ip_language_ex2;
        }
        public static string GetIPLanguage3()
        {
            return ip_language3;
        }
        public static string GetIPLanguageEx3()
        {
            return ip_language_ex3;
        }
        public static string GetTalkAbility()
        {
            return talk_ability;
        }
        public static string GetTalkAbilityEx()
        {
            return talk_ability_ex;
        }
        public static string GetIPMoney()
        {
            return ip_money;
        }
        public static string GetIPMoneyEx()
        {
            return ip_money_ex;
        }
        public static string GetIPInsurance()
        {
            return ip_insurance;
        }
        public static string GetIPInsuranceEx()
        {
            return ip_insurance_ex;
        }
        public static string GetFamilyProblem()
        {
            return family_problem;
        }
        public static string GetStatInfcHx()
        {
            return stat_infc_hx;
        }
        public static string GetStatINFC()
        {
            return statINFC;
        }
        public static string GetAfterAdmsHx()
        {
            return after_adms_hx;
        }
        public static string GetAfterADMS()
        {
            return afterADMS;
        }
        public static string GetIPPhoto()
        {
            return ip_photo;
        }
        public static string GetIPMargin()
        {
            return ip_margin;
        }

        public static string GetIPMarry()
        {
            return ip_marry;
        }
        public static string GetIPMarryEx()
        {
            return ip_marry_ex;
        }
        public static string GetMateName()
        {
            return mate_name;
        }
        public static string GetMateDuty()
        {
            return mate_duty;
        }
        public static string GetIPSon()
        {
            return ip_son;
        }
        public static string GetIPDaughter()
        {
            return ip_daughter;
        }
        public static string GetInReason()
        {
            return in_reason;
        }
        public static string GetInReasonEx()
        {
            return in_reason_ex;
        }
        public static string GetIPTransfer()
        {
            return ip_transfer;
        }
        public static string GetIPTransferEx()
        {
            return ip_transfer_ex;
        }
        public static string GetSignName()
        {
            return sign_name;
        }
        public static string GetSignRelate()
        {
            return sign_relate;
        }
        public static string GetSignAdr()
        {
            return sign_adr;
        }
        public static string GetSignTel()
        {
            return sign_tel;
        }
        public static string GetSignTel2()
        {
            return sign_tel2;
        }
        public static string GetSignTel3()
        {
            return sign_tel3;
        }
        public static string GetReName()
        {
            return re_name;
        }
        public static string GetReRelate()
        {
            return re_relate;
        }
        public static string GetReAdr()
        {
            return re_adr;
        }
        public static string GetReTel()
        {
            return re_tel;
        }
        public static string GetReTel2()
        {
            return re_tel2;
        }
        public static string GetReTel3()
        {
            return re_tel3;
        }
        public static string GetHurryName()
        {
            return hurry_name;
        }
        public static string GetHurryRelate()
        {
            return hurry_relate;
        }
        public static string GetHurryAdd()
        {
            return hurry_add;
        }
        public static string GetHurryTel()
        {
            return hurry_tel;
        }
        public static string GetHurryTel2()
        {
            return hurry_tel2;
        }
        public static string GetHurryTel3()
        {
            return hurry_tel3;
        }
        public static string GetEmrName()
        {
            return emr_name;
        }
        public static string GetEmrRelate()
        {
            return emr_relate;
        }
        public static string GetEmrAdd()
        {
            return emr_add;
        }
        public static string GetEmrTel()
        {
            return emr_tel;
        }
        public static string GetEmrTel2()
        {
            return emr_tel2;
        }
        public static string GetEmrTel3()
        {
            return emr_tel3;
        }

        public static string GetPayName()
        {
            return pay_name;
        }
        public static string GetPayTel()
        {
            return pay_tel;
        }
        public static string GetPayEvalute()
        {
            return pay_evalute;
        }
        public static string GetTransferName()
        {
            return transfer_name;
        }
        public static string GetIPIdentity()
        {
            return ip_identity;
        }

        //敬德要求新增的欄位
        public static string GetVisitLimit()
        {
            return visitlimit;
        }
        public static string GetSignID()
        {
            return signid;
        }
        public static string GetPayRelate()
        {
            return payrelate;
        }
        public static string GetPay2Name()
        {
            return pay2name;
        }
        public static string GetPay2Relate()
        {
            return pay2relate;
        }
        public static string GetPay2Tel()
        {
            return pay2tel;
        }
        public static string GetPay3Name()
        {
            return pay3name;
        }
        public static string GetPay3Relate()
        {
            return pay3relate;
        }
        public static string GetPay3Tel()
        {
            return pay3tel;
        }

        public static string GetWarning()
        {
            return warning;
        }
        

        public static string GetN1()
        {
            return nnselect;
        }
        public static string GetN2()
        {
            return nncount;
        }
        public static string GetN3()
        {
            return nnfrom;
        }

        /*
        public static string GetIPNO3()
        {
            return ipnoselect;
        }*/
        public static string GetIp_notemp()
        {
            return ip_no_temp;
        }
        public static string GetIp_emrname()
        {
            return ip_emrname;
        }
        public static string GetIp_emrtel()
        {
            return ip_emrtel;
        }
        public static string GetIp_emrtel2()
        {
            return ip_emrtel2;
        }
        public static string GetIp_emrtel3()
        {
            return ip_emrtel3;
        }
        public static string GetIp_emrelate()
        {
            return ip_emrelate;
        }
        public static string GetIp_visitdate()
        {
            return ip_visitdate;
        }
        public static string GetIp_exindate()
        {
            return ip_exindate;
        }
        public static string GetIp_innote()
        {
            return ip_innote;
        }

        public static string GetEmr2_NAME()
        {
            return emr2_name;
        }
        public static string GetEmr2_ADD()
        {
            return emr2_add;
        }
        public static string GetEmr2_TEL()
        {
            return emr2_tel;
        }
        public static string GetEmr2_TEL2()
        {
            return emr2_tel2;
        }
        public static string GetEmr2_TEL3()
        {
            return emr2_tel3;
        }
        public static string GetEmr2_RELATE()
        {
            return emr2_relate;
        }

        public static string GetSig_email()
        {
            return semail;
        }
        public static string GetInvo_email1()
        {
            return remail;
        }
        public static string GetHurry_email()
        {
            return hurryemail;
        }
        public static string GetHurry_email1()
        {
            return hurryemail1;
        }
        public static string GetHurry_email2()
        {
            return hurryemail2;
        }
        public static string GetPay_email()
        {
            return payemail1;
        }

        public static string GetSig_cutalkid()
        {
            return scutalkid;
        }
        public static string GetInvo_cutalkid()
        {
            return rcutalkid;
        }
        public static string GetHurry_cutalkid()
        {
            return hurrycutalkid;
        }
        public static string GetHurry_cutalkid1()
        {
            return hurrycutalkid1;
        }
        public static string GetHurry_cutalkid2()
        {
            return hurrycutalkid2;
        }
        public static string GetPay_cutalkid()
        {
            return paycutalkid;
        }

        //回傳膳食習慣
        public static string GetIPchklvege()
        {
            return chklvege_content;
        }
        public static string GetIPchklvege1()
        {
            return chklvege1_content;
        }
        public static string GetIPchblvege()
        {
            return chblvege_content;
        }
        public static string Getpickyfood()
        {
            return pickyfood;
        }
        public static string GetIPchklvegetime()
        {
            return chklvegetime_content;
        }
        public static string GetIPfavorbreak()
        {
            return favorbreak;
        }
        public static string GetIPfavorlunch()
        {
            return favorlunch;
        }
        public static string GetIPfavordinner()
        {
            return favordinner;
        }
        public static string GetIPforbid()
        {
            return forbid;
        }
        public static string GetIPspecorder()
        {
            return specorder;
        }
        public static string GetIPmealbreak()
        {
            return mealbreak;
        }
        public static string GetIPmealsnack()
        {
            return mealsnack;
        }
        public static string GetIPmeallunch()
        {
            return meallunch;
        }
        public static string GetIPmealtea()
        {
            return mealtea;
        }
        public static string GetIPmealdinner()
        {
            return mealdinner;
        }
        public static string GetIPmealmnsnack()
        {
            return mealmnsnack;
        }

        internal static string SearchHPInfo(string hp_name)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            //conn.ConnectionString = datasource;
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
            conn.Open();

            string hp_adr = "";
            //string searchhpinfo = "SELECT HOSPITAL_NAME, HOSPITAL_ADR FROM HOSPITAL WHERE HOSPITAL_NAME = '" + hp_name + "'";
            string searchhpinfo = "SELECT HP_ADD FROM HP_INFORMATION WHERE HP_NAME = '" + hp_name + "'";
            SqlCommand search = new SqlCommand(searchhpinfo, conn);
            SqlDataReader searchlist = search.ExecuteReader();
            while (searchlist.Read())
            {
                hp_adr = searchlist.GetValue(0).ToString().Trim();
            }
            conn.Close();
            return hp_adr;
        }
        
        //判斷住民編號是否重複(AccountEnable = 'Y')
        internal static bool CheckIPNO(string ipnonew)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            bool b;
            string searchipnonew = "SELECT IP_NO_NEW FROM IP_INFORMATION WHERE IP_NO_NEW = '" + ipnonew + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(searchipnonew, conn);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                b = true;
            }
            else 
            {
                b = false;
            }
            conn.Close();
            return b;
        }
        internal static string Search_ip_no_by_ip_no_new(string ip_no_new, string connection_id)
        {
            string a_ip_no = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO, IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO_NEW = '" + ip_no_new + "' AND AccountEnable = 'Y'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_no = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_no;
        }
        //判斷住民編號是否重複(AccountEnable = 'N')
        internal static bool CheckIPNO1(string ipnonew)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            bool b;
            string searchipnonew = "SELECT IP_NO_NEW FROM IP_INFORMATION WHERE IP_NO_NEW = '" + ipnonew + "' AND AccountEnable = 'N'";
            SqlCommand search = new SqlCommand(searchipnonew, conn);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                b = true;
            }
            else
            {
                b = false;
            }
            conn.Close();
            return b;
        }
        internal static string Search_ip_no_by_ip_no_new1(string ip_no_new, string connection_id)
        {
            string a_ip_no = "";
            SqlConnection con = new SqlConnection();
            con.Close();
            con.ConnectionString = HospitalConnect.ConnectionString.ReturnConnectionString(connection_id);
            con.Open();
            string strSQL = "SELECT IP_INFORMATION.IP_NO, IP_INFORMATION.IP_NAME FROM IP_INFORMATION WHERE IP_INFORMATION.IP_NO_NEW = '" + ip_no_new + "' AND AccountEnable = 'N'";
            SqlCommand search = new SqlCommand(strSQL, con);
            SqlDataReader searchlist = search.ExecuteReader();

            if (searchlist.Read())
            {
                a_ip_no = searchlist.GetValue(0).ToString();
            }
            searchlist.Close();
            con.Close();
            return a_ip_no;
        }

        //判斷住民是否已入住
        public static string CheckIPIn(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string HaveIP = "";
            string hacatch = "select Count(*) from ROOM R where R.IP_NO = '" + ipno + "'";
            SqlCommand checkip = new SqlCommand(hacatch, conn);
            haveone = Convert.ToInt32(checkip.ExecuteScalar().ToString());

            if (haveone >= 1)
            {
                HaveIP = "1";
            }
            else
                HaveIP = "0";
            conn.Close();
            return HaveIP;
        }

        //取得保證金資料
        public static string CheckIPMargin(string ipno)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string ip_margin = "";
            string searchmargin = "select CASH FROM IP_REC_CASH_NOW WHERE IP_NO = '" + ipno + "'";
            SqlCommand checkmargin = new SqlCommand(searchmargin, conn);
            SqlDataReader searchlist = checkmargin.ExecuteReader();

            while (searchlist.Read())
            {
                ip_margin = searchlist.GetValue(0).ToString().Trim();
            }
            conn.Close();
            return ip_margin;
        }

        internal static void cleanAllParameter()
        {
            ip_no = "";
            ip_no_new = "";
            ip_name= "";
            ip_sex= "";
            ip_id= "";
            ip_birthday= "";

            ip_country="";
            ip_arc="";
            ip_passport="";
            ip_residedate="";
            ip_dnr = "";

            //ip_blood= "";
            //ip_born= "";
            //ip_original= "";
            ip_height= "";
            ip_weight= "";
            ip_permadr= "";
            ip_nowadr= "";
            //health_card= "";
            ip_statues= "";
            ip_statues_ex= "";
            ip_barrier= "";
            check_method= "";
            ip_belief= "";
            ip_belief_ex= "";
            ip_education= "";
            ip_career= "";
            ip_career_ex= "";
            ip_language= "";
            ip_language_ex= "";
            ip_language2= "";
            ip_language_ex2= "";
            ip_language3= "";
            ip_language_ex3= "";
            talk_ability= "";
            talk_ability_ex = "";
            ip_money= "";
            ip_money_ex= "";
            ip_insurance= "";
            ip_insurance_ex = "";
            family_problem= "";
            stat_infc_hx= "";
            after_adms_hx= "";
            statINFC = "";
            afterADMS = "";
            ip_photo= "";
            ip_margin= "";

            ip_marry= "";
            ip_marry_ex = "";
            mate_name= "";
            mate_duty= "";
            ip_son= "";
            ip_daughter= "";
            in_reason= "";
            in_reason_ex = "";
            ip_transfer= "";
            ip_transfer_ex= "";
            sign_name= "";
            sign_relate= "";
            sign_adr= "";
            sign_tel= "";
            sign_tel2 = "";
            sign_tel3 = "";
            re_name= "";
            re_relate= "";
            re_adr= "";
            re_tel= "";
            re_tel2 = "";
            re_tel3 = "";
            hurry_name= "";
            hurry_relate = "";
            hurry_add = "";
            hurry_tel = "";
            hurry_tel2 = "";
            hurry_tel3 = "";
            emr_name = "";
            emr_relate = "";
            emr_add = "";
            emr_tel = "";
            emr_tel2 = "";
            emr_tel3 = "";
            pay_name= "";
            pay_tel= "";
            pay_evalute= "";
            transfer_name= "";

            ip_identity = "";

            nnselect= "";
            nncount= "";
            nnfrom= "";

            warning= "";
            sqlcom = "";
        }
        internal static void cleanAllParameterJ()
        {
            ip_no = "";
            ip_no_new = "";
            ip_name = "";
            ip_sex = "";
            ip_id = "";
            ip_birthday = "";

            ip_country = "";
            ip_arc = "";
            ip_passport = "";
            ip_residedate = "";
            ip_dnr = "";

            //ip_blood= "";
            //ip_born= "";
            //ip_original= "";
            ip_height = "";
            ip_weight = "";
            ip_permadr = "";
            ip_nowadr = "";
            //health_card= "";
            ip_statues = "";
            ip_statues_ex = "";
            ip_barrier = "";
            check_method = "";
            ip_belief = "";
            ip_belief_ex = "";
            ip_education = "";
            ip_career = "";
            ip_career_ex = "";
            ip_language = "";
            ip_language_ex = "";
            ip_language2 = "";
            ip_language_ex2 = "";
            ip_language3 = "";
            ip_language_ex3 = "";
            talk_ability = "";
            talk_ability_ex = "";
            ip_money = "";
            ip_money_ex = "";
            ip_insurance = "";
            ip_insurance_ex = "";
            family_problem = "";
            stat_infc_hx = "";
            after_adms_hx = "";
            statINFC = "";
            afterADMS = "";
            ip_photo = "";
            ip_margin = "";

            ip_marry = "";
            ip_marry_ex = "";
            mate_name = "";
            mate_duty = "";
            ip_son = "";
            ip_daughter = "";
            in_reason = "";
            in_reason_ex = "";
            ip_transfer = "";
            ip_transfer_ex = "";
            sign_name = "";
            sign_relate = "";
            sign_adr = "";
            sign_tel = "";
            sign_tel2 = "";
            sign_tel3 = "";
            re_name = "";
            re_relate = "";
            re_adr = "";
            re_tel = "";
            re_tel2 = "";
            re_tel3 = "";
            hurry_name = "";
            hurry_relate = "";
            hurry_add = "";
            hurry_tel = "";
            hurry_tel2 = "";
            hurry_tel3 = "";
            emr_name = "";
            emr_relate = "";
            emr_add = "";
            emr_tel = "";
            emr_tel2 = "";
            emr_tel3 = "";
            pay_name = "";
            pay_tel = "";
            pay_evalute = "";
            transfer_name = "";

            ip_identity = "";

            nnselect = "";
            nncount = "";
            nnfrom = "";

            warning = "";
            sqlcom = "";
        }
        //新增訪客資料
        public static void PotentialIPInfo(string tempipno, string ipname, string ipid, string birthday, string sex, string height, string weight, string nowadr, string marry, string inreason, string emrname, string emrtel, string emrtel2, string emrtel3, string relation, string visitdate, string expindate, string expinnote, string createdate, string createtime, string createuser)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string insertpotentip = "INSERT INTO POTENTIAL_IP_INFO (IP_NO_TEMP, IP_NAME, IP_ID, SEX, DOB, HEIGHT, WEIGHT, NOW_ADR, MARRY, IN_REASON, EMR_NAME, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR_RELATE, VISIT_DATE, EX_DATE, IN_NOTE, CREATE_DATE, CREATE_TIME, CREATE_USER, AccountEnable) VALUES ('" + tempipno + "', N'" + ipname + "', '" + ipid + "', '" + sex + "', '" + birthday + "', '" + height + "', '" + weight + "', '" + nowadr + "', '" + marry + "', '" + inreason + "', N'" + emrname + "', '" + emrtel + "', '" + emrtel2 + "', '" + emrtel3 + "', '" + relation + "', '" + visitdate + "', '" + expindate + "', N'" + expinnote + "', '" + createdate + "', '" + createtime + "', '" + createuser + "', 'Y')";
            SqlCommand insert = new SqlCommand(insertpotentip, conn);
            insert.ExecuteNonQuery();
            conn.Close();
            warning = "訪客資料新增完成";
        }

        //修改訪客資料-Update潛在客戶基本資料表
        public static void UpdatePotentialIPInfo(string no, string tempipno, string ipname, string ipid, string birthday, string sex, string height, string weight, string nowadr, string marry, string inreason, string emrname, string emrtel, string emrtel2, string emrtel3, string relation, string visitdate, string indate, string innote, string opdate, string optime, string opuser)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            
            string updatepotentip = "UPDATE POTENTIAL_IP_INFO SET IP_NAME = N'" + ipname + "', IP_ID = '" + ipid + "', SEX = '" + sex + "', DOB = '" + birthday + "', HEIGHT = '" + height + "', WEIGHT = '" + weight + "', NOW_ADR = '" + nowadr + "', MARRY = '" + marry + "', IN_REASON = '" + inreason + "', EMR_NAME = N'" + emrname + "', EMR_TEL = '" + emrtel + "', EMR_TEL2 = '" + emrtel2 + "', EMR_TEL3 = '" + emrtel3 + "', EMR_RELATE = '" + relation + "', VISIT_DATE = '" + visitdate + "', EX_DATE = '" + indate + "', IN_NOTE = N'" + innote + "', OP_DATE = '" + opdate + "', OP_TIME = '" + optime + "', OP_USER = '" + opuser + "' WHERE (NO = '" + no + "')";
            SqlCommand updatepip = new SqlCommand(updatepotentip, conn);
            updatepip.ExecuteNonQuery();
            conn.Close();
            warning = "訪客資料更新完成";
        }

        //依預約流水號及住民病歷號找訪客資料 public static void GetPotentialExpectData(string no, string ipname)
        public static void GetPotentialExpectData(string no)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();
            //string ipcatch = "select IP_NO_TEMP, IP_NAME, IP_ID, DOB, SEX, HEIGHT, WEIGHT, NOW_ADR, MARRY, IN_REASON, EMR_NAME, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR_RELATE, VISIT_DATE, EX_DATE, IN_NOTE from POTENTIAL_IP_INFO where NO = '" + no + "' AND IP_NAME = '" + ipname + "' AND AccountEnable = 'Y'";
            string ipcatch = "select IP_NO_TEMP, IP_NAME, IP_ID, DOB, SEX, HEIGHT, WEIGHT, NOW_ADR, MARRY, IN_REASON, EMR_NAME, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR_RELATE, VISIT_DATE, EX_DATE, IN_NOTE from POTENTIAL_IP_INFO where NO = '" + no + "' AND AccountEnable = 'Y'";
            SqlCommand ipinfo = new SqlCommand(ipcatch, conn);
            SqlDataReader iplist = ipinfo.ExecuteReader();

            if (iplist.Read())
            {
                ip_name = iplist.GetValue(1).ToString();
                ip_id = iplist.GetValue(2).ToString();
                ip_birthday = iplist.GetValue(3).ToString();
                ip_sex = iplist.GetValue(4).ToString();
                ip_height = iplist.GetValue(5).ToString();
                ip_weight = iplist.GetValue(6).ToString();
                ip_nowadr = iplist.GetValue(7).ToString();
                ip_marry = iplist.GetValue(8).ToString();
                in_reason = iplist.GetValue(9).ToString();
                ip_emrname = iplist.GetValue(10).ToString();
                ip_emrtel = iplist.GetValue(11).ToString();
                ip_emrtel2 = iplist.GetValue(12).ToString();
                ip_emrtel3 = iplist.GetValue(13).ToString();
                ip_emrelate = iplist.GetValue(14).ToString();
                ip_visitdate = iplist.GetValue(15).ToString();
                ip_exindate = iplist.GetValue(16).ToString();
                ip_innote = iplist.GetValue(17).ToString();
                warning = "";
            }
            else
            {
                ip_no_temp = "";
                ip_name = "";
                ip_id = "";
                ip_birthday = "";
                ip_sex = "";
                ip_height = "";
                ip_weight = "";
                ip_nowadr = "";
                ip_marry = "";
                in_reason = "";
                ip_emrname = "";
                ip_emrtel = "";
                ip_emrtel2 = "";
                ip_emrtel3 = "";
                ip_emrelate = "";
                ip_visitdate = "";
                ip_exindate = "";
                ip_innote = "";
                warning = "查無此訪客資料！";
            }
            conn.Close();
        }
        //刪除訪客紀錄
        public static void DelPotentialIP(string no, string ipname, string op_user, string op_date, string op_time)
        {
            SqlConnection conn = new SqlConnection();
            conn.Close();
            conn.ConnectionString = datasource;
            conn.Open();

            string delpotentip = "UPDATE POTENTIAL_IP_INFO SET AccountEnable = 'N', OP_DATE = '" + op_date + "', OP_TIME = '" + op_time + "', OP_USER = '" + op_user + "' WHERE (NO = '" + no + "') AND (IP_NAME = '" + ipname + "')";
            update = new SqlCommand(delpotentip, conn);
            //update.ExecuteScalar();
            update.ExecuteNonQuery();
            warning = "訪客資料已刪除";
            conn.Close();
        }

        public static DataTable SearchPIPInfo(string no, string ipname, string connection_id)
        {
            string sql = "SELECT NO, IP_NAME, IP_ID, DOB, SEX, HEIGHT, WEIGHT, NOW_ADR, MARRY, IN_REASON, EMR_NAME, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR_RELATE, VISIT_DATE, EX_DATE, IN_NOTE, CREATE_DATE, CREATE_USER, OP_DATE, OP_USER FROM POTENTIAL_IP_INFO WHERE NO = '" + no + "' AND IP_NAME = '" + ipname + "' AND AccountEnable = 'Y'";
            return sqlData.getDataTable(sql, connection_id);
        }

        public static DataTable GetPIPList(string lblsql, string connection_id)
        {
            //string sql = "SELECT NO, IP_NAME, IP_ID, DOB, SEX, HEIGHT, WEIGHT, NOW_ADR, MARRY, IN_REASON, EMR_NAME, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR_RELATE, VISIT_DATE, EX_DATE, EXPECT_IN FROM POTENTIAL_IP_INFO WHERE ((IN_CHECK IS NULL) OR (IN_CHECK = 'N')) AND " + lblsql + "  AND AccountEnable = 'Y'";
            string sql = "SELECT NO, IP_NAME, IP_ID, DOB, SEX, HEIGHT, WEIGHT, NOW_ADR, MARRY, IN_REASON, EMR_NAME, EMR_TEL, EMR_TEL2, EMR_TEL3, EMR_RELATE, VISIT_DATE, EX_DATE, EXPECT_IN, IN_CHECK FROM POTENTIAL_IP_INFO WHERE " + lblsql + "  AND AccountEnable = 'Y'";
            return sqlData.getDataTable(sql, connection_id);
        }

    }
}