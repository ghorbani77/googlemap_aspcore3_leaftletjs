using Darayas.Maps.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Darayas.Maps.Models.JsMethods
{
    public static class MsgBox
    {
        public static string Show(string Title, string Msg, MsgBoxType type, string CallBackFuncs = null)
        {
            Thread.Sleep(100);

            CallBackFuncs = CallBackFuncs ?? "function () { }";
            string Js = $@"swal({{
                                html: true,
                                title: '{Title.Replace("'", "`")}',
                                text: '{Msg.Replace("'", "`")}',
                                type: '{type.ToString()}',
                                confirmButtonText: 'باشه',
                             }},{CallBackFuncs});";
            return Js;
        }

        public static JsResult ShowSuccessMsg(string CallBackFuncs = null)
        {
            return new JsResult(Show("موفقیت آمیز", "عملیات با موفقیت انجام شد", MsgBoxType.success, CallBackFuncs));
        }

        public static JsResult ShowErrMsg(string CallBackFuncs = null)
        {
            return new JsResult(Show("خطا", "هنگام اجرای درخواست شما خطایی رخ داد", MsgBoxType.error, CallBackFuncs));
        }

        public static JsResult ShowModelStateMsg(string Error, string CallBackFuncs = null)
        {
            return new JsResult(Show("لطفا موارد زیر را برطرف نمایید", Error, MsgBoxType.warning, CallBackFuncs));
        }

        public static JsResult ShowErr500Msg(string CallBackFuncs = null)
        {
            return new JsResult(Show("خطا 500", "هنگام اجرای درخواست شما سرور دچار خطا شد", MsgBoxType.error, CallBackFuncs));
        }

        public enum MsgBoxType
        {
            success,
            error,
            warning,
            info
        }
    }
}
