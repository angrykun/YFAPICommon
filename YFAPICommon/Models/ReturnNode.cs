using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YFAPICommon.Models
{
    public class ReturnNode
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }


        public static ReturnNode ReturnSuccess()
        {
            ReturnNode node = new ReturnNode();
            node.Code = 0;
            node.Msg = "操作成功";
            return node;
        }

        public static ReturnNode ReturnSuccess(object data = null)
        {
            ReturnNode node = new ReturnNode();
            node.Code = 0;
            node.Msg = "操作成功";
            node.Data = data;
            return node;
        }

        public static ReturnNode ReturnSuccess(string msg, object data)
        {
            ReturnNode node = new ReturnNode();
            node.Code = 0;
            node.Msg = msg;
            node.Data = data;
            return node;
        }



        public static ReturnNode ReturnError()
        {
            ReturnNode node = new ReturnNode();
            node.Code = 1;
            node.Msg = "操作失败";
            return node;
        }

        public static ReturnNode ReturnError(string msg)
        {
            ReturnNode node = new ReturnNode();
            node.Code = 1;
            node.Msg = msg;
            return node;
        }

        public static ReturnNode ReturnError(int code, string msg)
        {
            ReturnNode node = new ReturnNode();
            node.Code = code;
            node.Msg = msg;
            return node;
        }

    }
}