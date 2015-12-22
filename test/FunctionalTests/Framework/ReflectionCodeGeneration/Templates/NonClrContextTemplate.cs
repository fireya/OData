﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.OData.Service;
using System.Collections.Generic;
using Microsoft.OData.Service.Providers;
using System.Data.Test.Astoria.NonClr;
using System.Reflection;
using System.Globalization;

namespace [[ContextNamespace]]
{
    public partial class [[ContextTypeName]] : [[Inheritance]]
    {
        public [[ContextTypeName]](object provider) : base(provider)
        {
        }

        public override void PopulateMetadata()
        {
            if (types == null)
            {
                types = new List<ResourceType>();
                containers = new List<ResourceSet>();

                [[GeneratedMetadata]]

                types.AddRange(addedTypes);
                containers.AddRange(addedContainers);
            }

            if (serviceOpCreateParams == null)
            {
                serviceOpCreateParams = new List<ServiceOperationCreationParams>();

                [[GeneratedServiceOperations]]
            }

            if (operations == null)
            {
                operations = new List<ServiceOperation>();
                foreach (ServiceOperationCreationParams creationParams in serviceOpCreateParams)
                {
                    this.AddServiceOperationInternal(creationParams.Name, creationParams.Kind, creationParams.TypeName, creationParams.Method);
                }
            }

            foreach (ResourceSet set in containers) { set.SetReadOnly(); }
            foreach (ResourceType type in types) { type.SetReadOnly(); }
            foreach (ServiceOperation operation in operations) { operation.SetReadOnly(); }
        }

        public override void AddServiceOperation(string name, ServiceOperationResultKind kind, string typeName, string verb)
        {
            serviceOpCreateParams.Add(new ServiceOperationCreationParams(name, kind, typeName, verb));
        }

        public void AddServiceOperationInternal(string name, ServiceOperationResultKind kind, string typeName, string verb)
        {
            MethodInfo method = typeof([[ServiceClassName]]).GetMethod(name, BindingFlags.Public | BindingFlags.Instance); // ArubaService
            ResourceType resourceType = null;
            Type returnType = null;

            if( kind != ServiceOperationResultKind.Void )
            {
                if( kind == ServiceOperationResultKind.DirectValue )
                    returnType = method.ReturnType;
                else
                    returnType = method.ReturnType.GetGenericArguments()[0];

                resourceType = ResourceType.GetPrimitiveResourceType(returnType);
                if (resourceType == null)
                    resourceType = types.Where(t => t.Name == typeName).First();
            }

            ServiceOperation newServiceOp = new ServiceOperation(method.Name, kind, resourceType, containers.FirstOrDefault(c => c.ResourceType == resourceType),
                verb, this.GetParameters(method));

            SetMemberMimeType(method, newServiceOp);
            newServiceOp.SetReadOnly();
            operations.Add(newServiceOp);
        }

        private ServiceOperationParameter[] GetParameters(MethodInfo method)
        {
            ParameterInfo[] parametersInfo = method.GetParameters();
            ServiceOperationParameter[] parameters = new ServiceOperationParameter[parametersInfo.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                string parameterName = parametersInfo[i].Name ?? "p" + i.ToString(CultureInfo.InvariantCulture);
                parameters[i] = new ServiceOperationParameter(parameterName, ResourceType.GetPrimitiveResourceType(parametersInfo[i].ParameterType));
            }

            return parameters;
        }

        private static void SetMemberMimeType(MemberInfo member, ServiceOperation operation)
        {
            MimeTypeAttribute attribute = member.ReflectedType.GetCustomAttributes(typeof(MimeTypeAttribute), true)
                .Cast<MimeTypeAttribute>()
                .SingleOrDefault(o => o.MemberName == member.Name);
            if (attribute != null) { operation.MimeType = attribute.MimeType; }
        }
    }

[[ClrBackingTypes]]
}
