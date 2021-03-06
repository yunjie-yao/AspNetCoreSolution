﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YangXuAPI.Entities;
using YangXuAPI.ValidationAttributes;

namespace YangXuAPI.Models
{
    //验证优先级：属性级别，类级别（DataAnnotations，IValidatableObject，ValidationAttributes）
    
    public class EmployeeAddDto:EmployeeAbstractDto
    {
        [Display(Name = "员工编号")]
        [Required(ErrorMessage = "{0}字段必填")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}字段长度必须为{1}")]
        public override string EmployeeNo { get; set; }
    }
}

//FluentValidation！！！（查查怎么写）
//1.创建复杂验证
//2.验证规则和Model分离
//3.容易单元测试