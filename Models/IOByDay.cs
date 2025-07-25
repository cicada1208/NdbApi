using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_IOByDay")]
    public class IOByDay
    {
        [Key]
        public string ptEncounterID { get; set; }

        [Key]
        public string REC_DATE { get; set; }

        public string PT_NO { get; set; }

        public string T_Diet_D { get; set; }

        public string T_Diet_D_S { get; set; }

        public string T_Diet_E { get; set; }

        public string T_Diet_E_S { get; set; }

        public string T_Diet_N { get; set; }

        public string T_Diet_N_S { get; set; }

        public string T_Diet_T { get; set; }

        public string T_Diet_T_S { get; set; }

        public string T_GFI_D { get; set; }

        public string T_GFI_D_S { get; set; }

        public string T_GFI_E { get; set; }

        public string T_GFI_E_S { get; set; }

        public string T_GFI_N { get; set; }

        public string T_GFI_N_S { get; set; }

        public string T_GFI_T { get; set; }

        public string T_GFI_T_S { get; set; }

        public string T_TFI_D { get; set; }

        public string T_TFI_D_S { get; set; }

        public string T_TFI_E { get; set; }

        public string T_TFI_E_S { get; set; }

        public string T_TFI_N { get; set; }

        public string T_TFI_N_S { get; set; }

        public string T_TFI_T { get; set; }

        public string T_TFI_T_S { get; set; }

        public string T_Sustenance_D { get; set; }

        public string T_Sustenance_D_S { get; set; }

        public string T_Sustenance_E { get; set; }

        public string T_Sustenance_E_S { get; set; }

        public string T_Sustenance_N { get; set; }

        public string T_Sustenance_N_S { get; set; }

        public string T_Sustenance_T { get; set; }

        public string T_Sustenance_T_S { get; set; }

        public string T_BTF_D { get; set; }

        public string T_BTF_D_S { get; set; }

        public string T_BTF_E { get; set; }

        public string T_BTF_E_S { get; set; }

        public string T_BTF_N { get; set; }

        public string T_BTF_N_S { get; set; }

        public string T_BTF_T { get; set; }

        public string T_BTF_T_S { get; set; }

        public string T_OtherIntake_D { get; set; }

        public string T_OtherIntake_D_S { get; set; }

        public string T_OtherIntake_E { get; set; }

        public string T_OtherIntake_E_S { get; set; }

        public string T_OtherIntake_N { get; set; }

        public string T_OtherIntake_N_S { get; set; }

        public string T_OtherIntake_T { get; set; }

        public string T_OtherIntake_T_S { get; set; }

        public string T_Intake_D { get; set; }

        public string T_Intake_E { get; set; }

        public string T_Intake_N { get; set; }

        public string T_Intake_T { get; set; }

        public string T_Urinary_D { get; set; }

        public string T_Urinary_D2 { get; set; }

        public string T_Urinary_D_S { get; set; }

        public string T_Urinary_E { get; set; }

        public string T_Urinary_E2 { get; set; }

        public string T_Urinary_E_S { get; set; }

        public string T_Urinary_N { get; set; }

        public string T_Urinary_N2 { get; set; }

        public string T_Urinary_N_S { get; set; }

        public string T_Urinary_T { get; set; }

        public string T_Urinary_T2 { get; set; }

        public string T_Urinary_T_S { get; set; }

        public string T_Stool_D { get; set; }

        public string T_Stool_D2 { get; set; }

        public string T_Stool_D_S { get; set; }

        public string T_Stool_E { get; set; }

        public string T_Stool_E2 { get; set; }

        public string T_Stool_E_S { get; set; }

        public string T_Stool_N { get; set; }

        public string T_Stool_N2 { get; set; }

        public string T_Stool_N_S { get; set; }

        public string T_Stool_T { get; set; }

        public string T_Stool_T2 { get; set; }

        public string T_Stool_T_S { get; set; }

        public string T_ThrowUp_D { get; set; }

        public string T_ThrowUp_D2 { get; set; }

        public string T_ThrowUp_D_S { get; set; }

        public string T_ThrowUp_E { get; set; }

        public string T_ThrowUp_E2 { get; set; }

        public string T_ThrowUp_E_S { get; set; }

        public string T_ThrowUp_N { get; set; }

        public string T_ThrowUp_N2 { get; set; }

        public string T_ThrowUp_N_S { get; set; }

        public string T_ThrowUp_T { get; set; }

        public string T_ThrowUp_T2 { get; set; }

        public string T_ThrowUp_T_S { get; set; }

        public string T_PleuralEffsion_D { get; set; }

        public string T_PleuralEffsion_D_S { get; set; }

        public string T_PleuralEffsion_E { get; set; }

        public string T_PleuralEffsion_E_S { get; set; }

        public string T_PleuralEffsion_N { get; set; }

        public string T_PleuralEffsion_N_S { get; set; }

        public string T_PleuralEffsion_T { get; set; }

        public string T_PleuralEffsion_T_S { get; set; }

        public string T_Ascites_D { get; set; }

        public string T_Ascites_D_S { get; set; }

        public string T_Ascites_E { get; set; }

        public string T_Ascites_E_S { get; set; }

        public string T_Ascites_N { get; set; }

        public string T_Ascites_N_S { get; set; }

        public string T_Ascites_T { get; set; }

        public string T_Ascites_T_S { get; set; }

        public string T_WDEx_D { get; set; }

        public string T_WDEx_D_S { get; set; }

        public string T_WDEx_E { get; set; }

        public string T_WDEx_E_S { get; set; }

        public string T_WDEx_N { get; set; }

        public string T_WDEx_N_S { get; set; }

        public string T_WDEx_T { get; set; }

        public string T_WDEx_T_S { get; set; }

        public string T_Drainage_D { get; set; }

        public string T_Drainage_D_S { get; set; }

        public string T_Drainage_E { get; set; }

        public string T_Drainage_E_S { get; set; }

        public string T_Drainage_N { get; set; }

        public string T_Drainage_N_S { get; set; }

        public string T_Drainage_T { get; set; }

        public string T_Drainage_T_S { get; set; }

        public string T_OtherOutput_D { get; set; }

        public string T_OtherOutput_D_S { get; set; }

        public string T_OtherOutput_E { get; set; }

        public string T_OtherOutput_E_S { get; set; }

        public string T_OtherOutput_N { get; set; }

        public string T_OtherOutput_N_S { get; set; }

        public string T_OtherOutput_T { get; set; }

        public string T_OtherOutput_T_S { get; set; }

        public string T_Output_D { get; set; }

        public string T_Output_E { get; set; }

        public string T_Output_N { get; set; }

        public string T_Output_T { get; set; }

        public string T_IODif_D { get; set; }

        public string T_IODif_E { get; set; }

        public string T_IODif_N { get; set; }

        public string T_IODif_T { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_NAME { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string T_SurgeryIn_D { get; set; }

        public string T_SurgeryIn_D_S { get; set; }

        public string T_SurgeryIn_E { get; set; }

        public string T_SurgeryIn_E_S { get; set; }

        public string T_SurgeryIn_N { get; set; }

        public string T_SurgeryIn_N_S { get; set; }

        public string T_SurgeryIn_T { get; set; }

        public string T_SurgeryIn_T_S { get; set; }

        public string T_SurgeryOut_D { get; set; }

        public string T_SurgeryOut_D_S { get; set; }

        public string T_SurgeryOut_E { get; set; }

        public string T_SurgeryOut_E_S { get; set; }

        public string T_SurgeryOut_N { get; set; }

        public string T_SurgeryOut_N_S { get; set; }

        public string T_SurgeryOut_T { get; set; }

        public string T_SurgeryOut_T_S { get; set; }

    }
}

