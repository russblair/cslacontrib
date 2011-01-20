﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaContrib.Properties;

namespace CslaContrib.Rules.CommonRules
{
  #region Comparable Field Rules
  /// <summary>
  /// Validates that primary property is less than compareToProperty
  /// </summary>
  public class LessThan : CommonBusinessRule
  {
    private IPropertyInfo CompareTo { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LessThan"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    /// <param name="compareToProperty">The compare to property.</param>
    public LessThan(IPropertyInfo primaryProperty, IPropertyInfo compareToProperty)
      : base(primaryProperty)
    {
      CompareTo = compareToProperty;
      InputProperties = new List<IPropertyInfo>() { primaryProperty, compareToProperty };
    }

    /// <summary>
    /// Does the check for primary propert less than compareTo property
    /// </summary>
    /// <param name="context">Rule context object.</param>
    protected override void Execute(RuleContext context)
    {
      var value1 = (IComparable)context.InputPropertyValues[PrimaryProperty];
      var value2 = (IComparable)context.InputPropertyValues[CompareTo];

      if (value1.CompareTo(value2) >= 0)
      {
        context.AddErrorResult(string.Format(Resources.LessThanRule, PrimaryProperty.FriendlyName, CompareTo.FriendlyName));
      }
    }
  }

  /// <summary>
  /// Validates that primary property is less than or equal compareToProperty
  /// </summary>
  public class LessThanOrEqual : CommonBusinessRule
  {
    private IPropertyInfo CompareTo { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LessThanOrEqual"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    /// <param name="compareToProperty">The compare to property.</param>
    public LessThanOrEqual(IPropertyInfo primaryProperty, IPropertyInfo compareToProperty)
      : base(primaryProperty)
    {
      CompareTo = compareToProperty;
      InputProperties = new List<IPropertyInfo>() { primaryProperty, compareToProperty };
    }

    /// <summary>
    /// Does the check for primary propert less than compareTo property
    /// </summary>
    /// <param name="context">Rule context object.</param>
    protected override void Execute(RuleContext context)
    {
      var value1 = (IComparable)context.InputPropertyValues[PrimaryProperty];
      var value2 = (IComparable)context.InputPropertyValues[CompareTo];

      if (value1.CompareTo(value2) > 0)
      {
        context.AddErrorResult(string.Format(Resources.LessThanOrEqualRule, PrimaryProperty.FriendlyName, CompareTo.FriendlyName));
      }
    }
  }

  /// <summary>
  /// Validates that primary property is greater than compareToProperty
  /// </summary>
  public class GreaterThan : CommonBusinessRule
  {
    private IPropertyInfo CompareTo { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GreaterThan"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    /// <param name="compareToProperty">The compare to property.</param>
    public GreaterThan(IPropertyInfo primaryProperty, IPropertyInfo compareToProperty)
      : base(primaryProperty)
    {
      CompareTo = compareToProperty;
      InputProperties = new List<IPropertyInfo>() { primaryProperty, compareToProperty };
    }

    /// <summary>
    /// Does the check for primary propert less than compareTo property
    /// </summary>
    /// <param name="context">Rule context object.</param>
    protected override void Execute(RuleContext context)
    {
      var value1 = (IComparable)context.InputPropertyValues[PrimaryProperty];
      var value2 = (IComparable)context.InputPropertyValues[CompareTo];

      if (value1.CompareTo(value2) <= 0)
      {
        context.AddErrorResult(string.Format(Resources.GreaterThanRule, PrimaryProperty.FriendlyName, CompareTo.FriendlyName));
      }
    }
  }


  /// <summary>
  /// Validates that primary property is freater than or equal compareToProperty
  /// </summary>
  public class GreaterThanOrEqual : CommonBusinessRule
  {
    private IPropertyInfo CompareTo { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GreaterThanOrEqual"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    /// <param name="compareToProperty">The compare to property.</param>
    public GreaterThanOrEqual(IPropertyInfo primaryProperty, IPropertyInfo compareToProperty)
      : base(primaryProperty)
    {
      CompareTo = compareToProperty;
      InputProperties = new List<IPropertyInfo>() { primaryProperty, compareToProperty };
    }

    /// <summary>
    /// Does the check for primary propert less than compareTo property
    /// </summary>
    /// <param name="context">Rule context object.</param>
    protected override void Execute(RuleContext context)
    {
      var value1 = (IComparable)context.InputPropertyValues[PrimaryProperty];
      var value2 = (IComparable)context.InputPropertyValues[CompareTo];

      if (value1.CompareTo(value2) < 0)
      {
        context.AddErrorResult(string.Format(Resources.GreaterThanOrEqualRule, PrimaryProperty.FriendlyName, CompareTo.FriendlyName));
      }
    }
  }


  #endregion

  #region Flow Control Rules

  /// <summary>
  /// Rule indicating whether the user is authorized
  /// to change the property value.
  /// Will always be silent and never set rule to broken.
  /// </summary>
  /// <remarks>
  /// Combine this Rule with short-circuiting to
  /// prevent evaluation of other rules in the case
  /// that the user isn't allowed to change the value.
  /// </remarks>
  public class StopIfNotCanWrite : BusinessRule
  {
    public StopIfNotCanWrite(IPropertyInfo property) : base(property) { }

    protected override void Execute(RuleContext context)
    {
      var target = (Csla.Core.BusinessBase)context.Target;
      if (!target.CanWriteProperty(context.Rule.PrimaryProperty))
      {
        context.AddSuccessResult(true);
      }
    }
  }

  /// <summary>
  /// Rule indicating whether the user is authorized
  /// to change the property value.
  /// Will always be silent and never set rule to broken.
  /// </summary>
  /// <remarks>
  /// Combine this Rule with short-circuiting to
  /// prevent evaluation of other rules in the case
  /// that the user isn't allowed to change the value.
  /// </remarks>
  public class StopIfNotIsNew : BusinessRule
  {
    public StopIfNotIsNew(IPropertyInfo property) : base(property) { }

    protected override void Execute(RuleContext context)
    {
      var target = (Csla.Core.ITrackStatus)context.Target;
      if (!target.IsNew)
      {
        context.AddSuccessResult(true);
      }
    }
  }

  /// <summary>
  /// Rule indicating whether the user is authorized
  /// to change the property value.
  /// Will always be silent and never set rule to broken.
  /// </summary>
  /// <remarks>
  /// Combine this Rule with short-circuiting to
  /// prevent evaluation of other rules in the case
  /// that the user isn't allowed to change the value.
  /// </remarks>  
  public class StopIfNotIsExisting : BusinessRule
  {
    public StopIfNotIsExisting(IPropertyInfo property) : base(property) { }

    protected override void Execute(RuleContext context)
    {
      var target = (Csla.Core.ITrackStatus)context.Target;
      if (target.IsNew)
      {
        context.AddSuccessResult(true);
      }
    }
  }

  #endregion

  #region Transformation Rules

  /// <summary>
  /// CalcSum rule will set primary property to the sum of all supplied properties.
  /// </summary>
  public class CalcSum : BusinessRule
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CalcSum"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    /// <param name="inputProperties">The input properties.</param>
    public CalcSum(IPropertyInfo primaryProperty, params IPropertyInfo[] inputProperties)
      : base(primaryProperty)
    {
      InputProperties = new List<IPropertyInfo>();
      InputProperties.AddRange(inputProperties);
    }

    protected override void Execute(RuleContext context)
    {
      // Use linq Sum to calculate the sum value 
      var sum = context.InputPropertyValues.Sum(property => (dynamic)property.Value);

      // add calculated value to OutValues 
      // When rule is completed the RuleEngig will update businessobject
      context.AddOutValue(PrimaryProperty, sum);
    }
  }

  /// <summary>
  /// makes sure the property is formatted as uppercase string.
  /// </summary>
  public class ToUpperCase : BusinessRule
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ToUpperCase"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    public ToUpperCase(IPropertyInfo primaryProperty)
      : base(primaryProperty)
    {
      InputProperties = new List<IPropertyInfo>() { primaryProperty };
    }

    protected override void Execute(RuleContext context)
    {
      var value = (string)context.InputPropertyValues[PrimaryProperty];
      if (string.IsNullOrEmpty(value)) return;

      var newValue = value.ToUpper();
      context.AddOutValue(PrimaryProperty, newValue);
    }
  }


  /// <summary>
  /// makes sure the property is formatted as uppercase string.
  /// </summary>
  public class ToLowerCase : BusinessRule
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ToLowerCase"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
    public ToLowerCase(IPropertyInfo primaryProperty)
      : base(primaryProperty)
    {
      InputProperties = new List<IPropertyInfo>() { primaryProperty };
    }

    protected override void Execute(RuleContext context)
    {
      var value = (string)context.InputPropertyValues[PrimaryProperty];
      if (string.IsNullOrEmpty(value)) return;

      var newValue = value.ToLower();
      context.AddOutValue(PrimaryProperty, newValue);
    }
  }

  /// <summary>
  /// Removes leading, trailing and duplicate spaces.
  /// </summary>
  public class RemoveExtraSpace : BusinessRule
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveExtraSpace"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
      public RemoveExtraSpace(IPropertyInfo primaryProperty)
      : base(primaryProperty)
    {
      InputProperties = new List<IPropertyInfo>() { primaryProperty };
    }

    protected override void Execute(RuleContext context)
    {
      var value = (string)context.InputPropertyValues[PrimaryProperty];
      if (string.IsNullOrEmpty(value)) return;

      var newValue = value.Trim(' ');
      var r = new Regex(@" +");
      newValue = r.Replace(newValue, @" ");
      context.AddOutValue(PrimaryProperty, newValue);
    }
  }

  /// <summary>
  /// Removes leading, trailing, duplicate spaces and all white space characters.
  /// </summary>
  public class RemoveWhiteSpace : BusinessRule
  {

    /// <summary>
      /// Initializes a new instance of the <see cref="RemoveWhiteSpace"/> class.
    /// </summary>
    /// <param name="primaryProperty">The primary property.</param>
      public RemoveWhiteSpace(IPropertyInfo primaryProperty)
      : base(primaryProperty)
    {
      InputProperties = new List<IPropertyInfo>() { primaryProperty };
    }

    protected override void Execute(RuleContext context)
    {
      var value = (string)context.InputPropertyValues[PrimaryProperty];
      if (string.IsNullOrEmpty(value)) return;

      var newValue = value.Trim();
      var r = new Regex(@"\s+");
      newValue = r.Replace(newValue, @" ");
      context.AddOutValue(PrimaryProperty, newValue);
    }
  }

  #endregion
}
