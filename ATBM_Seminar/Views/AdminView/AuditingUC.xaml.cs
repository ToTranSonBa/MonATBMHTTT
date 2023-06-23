using ATBM_Seminar.Models;
using ATBM_Seminar.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATBM_Seminar.Views.AdminView
{
    /// <summary>
    /// Interaction logic for AuditingUC.xaml
    /// </summary>
    public partial class AuditingUC : UserControl
    {
        private readonly AdminMV _adminMV;
        private List<Auditting_Class> _auditting_Classes { get; set; }
        private List<keyValue> decsriptionPolicyAuditing { get; set; }
        public AuditingUC(AdminMV adminMV)
        {
            _adminMV = adminMV;
            _auditting_Classes = new List<Auditting_Class>();
            InitializeComponent();
            setParamater();
        }

        private void setParamater()
        {
            decsriptionPolicyAuditing = new List<keyValue>();
            decsriptionPolicyAuditing.Add(new keyValue { 
                key = "AUDITING_PHANCONG_THOIGIAN", 
                value = "Những người đã cập nhật trường THOIGIAN trong quan hệ PHANCONG." 
            });
            decsriptionPolicyAuditing.Add(new keyValue { key = "AUDITING_NHANVIEN_LUONG_PHUCAP_NGUOIKHAC", value = "Những người đã đọc trên trường LUONG và PHUCAP của người khác." });
            decsriptionPolicyAuditing.Add(new keyValue { key = "AUDITING_NHANVIEN_CAPNHAT_LUONG_PHUCAP_NOT_TAICHINH", value = "Một người không thuộc vai trò “Tài chính” nhưng đã cập nhật thành công trên trường LUONG và PHUCAP." });
            decsriptionPolicyAuditing.Add(new keyValue { key = "AUD$", value = "Kiểm tra nhật ký hệ thống."});

            policy_auditing.ItemsSource = decsriptionPolicyAuditing.Select(p => p.key).ToList();
        }
        private void policy_auditing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var key = policy_auditing.SelectedValue as string;
            if (key != null)
            {
                if(key == "AUD$")
                {
                    _auditting_Classes = _adminMV.GetAudittingOnSystemLog();
                }
                else
                {
                    _auditting_Classes = _adminMV.GetAuditting_Classes(key);
                }
                auditingDataGrid.ItemsSource = _auditting_Classes;
                var value = decsriptionPolicyAuditing.SingleOrDefault(p => p.key == key).value;
                decriptionPolicyTextBlock.Text = value;
            }
        }

        public class keyValue
        {
            public string key { get; set; }
            public string value { get; set; }
        }


    }
}