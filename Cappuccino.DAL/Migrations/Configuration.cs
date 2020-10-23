using Cappuccino.Common.Enum;
using Cappuccino.Common.Util;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Cappuccino.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Cappuccino.DAL.EfDbContext>
    {
        public Configuration()
        {
            //����
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Cappuccino.DAL.EfDbContext context)
        {
            #region �û�����
            string salt = VerifyCodeUtils.CreateVerifyCode(5);
            string passwordHash = Md5Utils.EncryptTo32(salt + "123456");
            var sysUsers = new List<SysUser>
            {
              new SysUser{UserName="admin",NickName="��������Ա",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="admin@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Valid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysUser{UserName="system",NickName="ϵͳ����Ա",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="system@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysUser{UserName="user",NickName="��ͨ�û�",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="user@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysUser{UserName="test",NickName="�����û�",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="test@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };
            sysUsers.ForEach(s => context.Set<SysUser>().Add(s));
            context.SaveChanges();
            #endregion

            #region ��ɫ����
            var sysRoles = new List<SysRole>
            {
              new SysRole{  Name="��������Ա",Code="Administrator",EnabledMark=(int)EnabledMarkEnum.Valid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysRole{  Name="ϵͳ����Ա",Code="system",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysRole{  Name="��ͨ�û�",Code="user",EnabledMark=(int)EnabledMarkEnum.Invalid,Remark="ֻ�в鿴ҳ���Ȩ��",CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysRole{  Name="�����û�",Code="test",EnabledMark=(int)EnabledMarkEnum.Invalid,Remark="�������Ե�",CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

            };
            sysRoles.ForEach(s => context.Set<SysRole>().Add(s));
            context.SaveChanges();
            #endregion

            #region �û���ɫ����
            var adminUser = context.Set<SysUser>().Single(s => s.UserName == "admin");
            var adminRole = context.Set<SysRole>().Single(s => s.Code == "Administrator");
            adminUser.SysRoles.Add(adminRole);
            var systemUser = context.Set<SysUser>().Single(s => s.UserName == "system");
            var systemRole = context.Set<SysRole>().Single(s => s.Code == "system");
            systemUser.SysRoles.Add(systemRole);
            var user = context.Set<SysUser>().Single(s => s.UserName == "user");
            var userRole = context.Set<SysRole>().Single(s => s.Code == "user");
            user.SysRoles.Add(userRole);
            var testUser = context.Set<SysUser>().Single(s => s.UserName == "test");
            var testRole = context.Set<SysRole>().Single(s => s.Code == "test");
            testUser.SysRoles.Add(testRole);
            context.SaveChanges();
            #endregion

            #region Ȩ�޹���
            var sysActions = new List<SysAction>
            {
                   new SysAction{Id=1,Name="ϵͳ����",Code="system",ParentId=0,Type=0,SortCode=1,
                   SysActionMenu=new SysActionMenu{Icon="layui-icon-set-fill" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=2,Name="�û�����",Code="system.user.list",ParentId=1,Type=0,SortCode=1
                   ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysUser" }
                   ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=8,Name="����",Code="system.user.create",ParentId=2,Type=ActionTypeEnum.Button,SortCode=1,
                   SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=9,Name="�༭",Code="system.user.edit",ParentId=2,Type=ActionTypeEnum.Button,SortCode=2,
                   SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=10,Name="ɾ��",Code="system.user.delete",ParentId=2,Type=ActionTypeEnum.Button,SortCode=3,
                   SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=11,Name="����ɾ��",Code="system.user.batchDel",ParentId=2,Type=ActionTypeEnum.Button,SortCode=4,
                   SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=12,Name="����Ȩ��",Code="system.user.assign",ParentId=2,Type=ActionTypeEnum.Button,SortCode=5,
                   SysActionButton=new SysActionButton{ButtonCode="assign",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-warming pear-btn-sm",ButtonIcon="layui-icon-vercode" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                   new SysAction{Id=3,Name="��ɫ����",Code="system.role.list",ParentId=1,Type=0,SortCode=2
                   ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysRole" }
                   ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=13,Name="����",Code="system.role.create",ParentId=3,Type=ActionTypeEnum.Button,SortCode=1,
                   SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=14,Name="�༭",Code="system.role.edit",ParentId=3,Type=ActionTypeEnum.Button,SortCode=2,
                   SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=15,Name="ɾ��",Code="system.role.delete",ParentId=3,Type=ActionTypeEnum.Button,SortCode=3,
                   SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=16,Name="����ɾ��",Code="system.role.batchDel",ParentId=3,Type=ActionTypeEnum.Button,SortCode=4,
                   SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=17,Name="����Ȩ��",Code="system.role.assign",ParentId=3,Type=ActionTypeEnum.Button,SortCode=5,
                   SysActionButton=new SysActionButton{ButtonCode="assign",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-warming pear-btn-sm",ButtonIcon="layui-icon-vercode" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                   new SysAction{Id=4,Name="Ȩ�޹���",Code="system.action.list",ParentId=1,Type=0,SortCode=3
                   ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysAction" }
                   ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=18,Name="����",Code="system.action.create",ParentId=4,Type=ActionTypeEnum.Button,SortCode=1,
                   SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=19,Name="�༭",Code="system.action.edit",ParentId=4,Type=ActionTypeEnum.Button,SortCode=2,
                   SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=20,Name="ɾ��",Code="system.action.delete",ParentId=4,Type=ActionTypeEnum.Button,SortCode=3,
                   SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=21,Name="����ɾ��",Code="system.action.batchDel",ParentId=4,Type=ActionTypeEnum.Button,SortCode=4,
                   SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                   new SysAction{Id=5,Name="�����ֵ�",Code="system.dict.list",ParentId=1,Type=0,SortCode=4
                   ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysDict" }
                   ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=22,Name="����",Code="system.dict.create",ParentId=5,Type=ActionTypeEnum.Button,SortCode=1,
                   SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=23,Name="�༭",Code="system.dict.edit",ParentId=5,Type=ActionTypeEnum.Button,SortCode=2,
                   SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=24,Name="ɾ��",Code="system.dict.delete",ParentId=5,Type=ActionTypeEnum.Button,SortCode=3,
                   SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=25,Name="����ɾ��",Code="system.dict.batchDel",ParentId=5,Type=ActionTypeEnum.Button,SortCode=4,
                   SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                   new SysAction{Id=6,Name="��־����",Code="system.log",ParentId=1,Type=0,SortCode=5
                   ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry"}
                   ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                   new SysAction{Id=7,Name="��¼��־",Code="system.log.logon",ParentId=6,Type=0,SortCode=6
                   ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysLogLogon" }
                   ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                   new SysAction{Id=26,Name="����",Code="system.log.logon.export",ParentId=7,Type=ActionTypeEnum.Button,SortCode=1,
                   SysActionButton=new SysActionButton{ButtonCode="export",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-export" },
                   CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };

            sysActions.ForEach(s => context.Set<SysAction>().Add(s));
            context.SaveChanges();
            #endregion

            #region ����Ȩ��
            //��������Ա
            adminRole = context.Set<SysRole>().Single(s => s.Code == "Administrator");
            var adminAction = context.Set<SysAction>().ToList();
            adminAction.ForEach(x => adminRole.SysActions.Add(x));
            //��ͨ�û�
            userRole = context.Set<SysRole>().Single(s => s.Code == "user");
            adminAction = context.Set<SysAction>().Where(x => x.Type == ActionTypeEnum.Menu).ToList();
            adminAction.ForEach(x => adminRole.SysActions.Add(x));
            context.SaveChanges();
            #endregion

            #region �����ֵ�
            var sysDictTypes = new List<SysDictType>
            {
                new SysDictType{Name="��������",Code="OrganizeCategory",SortCode=1,SysDicts=new List<SysDict> {
                    new SysDict{Name="��˾",Code="Company",SortCode=1,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                    new SysDict{Name="����",Code="Department",SortCode=2,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                    new SysDict{Name="С��",Code="WorkGroup",SortCode=3,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1}},
                    CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysDictType{Name="�Ա�",Code="Sex",SortCode=2,SysDicts=new List<SysDict> {
                    new SysDict{Name="��",Code="Male",SortCode=1,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                    new SysDict{Name="Ů",Code="Female",SortCode=2,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1}},
                    CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };
            sysDictTypes.ForEach(s => context.Set<SysDictType>().Add(s));
            context.SaveChanges();
            #endregion
        }
    }
}
