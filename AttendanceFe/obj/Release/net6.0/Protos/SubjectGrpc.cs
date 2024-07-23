// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/subject.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace AttendanceFe.Protos {
  public static partial class SubjectRPC
  {
    static readonly string __ServiceName = "subject.SubjectRPC";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.SubjectRequest> __Marshaller_subject_SubjectRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.SubjectRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.SubjectResponse> __Marshaller_subject_SubjectResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.SubjectResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.SubjectRequestAdd> __Marshaller_subject_SubjectRequestAdd = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.SubjectRequestAdd.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.MessageReponse> __Marshaller_subject_MessageReponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.MessageReponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.SubjectRequestUpdate> __Marshaller_subject_SubjectRequestUpdate = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.SubjectRequestUpdate.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.Empty> __Marshaller_subject_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::AttendanceFe.Protos.SubjectListResponse> __Marshaller_subject_SubjectListResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AttendanceFe.Protos.SubjectListResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::AttendanceFe.Protos.SubjectRequest, global::AttendanceFe.Protos.SubjectResponse> __Method_GetSubjects = new grpc::Method<global::AttendanceFe.Protos.SubjectRequest, global::AttendanceFe.Protos.SubjectResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSubjects",
        __Marshaller_subject_SubjectRequest,
        __Marshaller_subject_SubjectResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::AttendanceFe.Protos.SubjectRequestAdd, global::AttendanceFe.Protos.MessageReponse> __Method_CreateSubject = new grpc::Method<global::AttendanceFe.Protos.SubjectRequestAdd, global::AttendanceFe.Protos.MessageReponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateSubject",
        __Marshaller_subject_SubjectRequestAdd,
        __Marshaller_subject_MessageReponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::AttendanceFe.Protos.SubjectRequestUpdate, global::AttendanceFe.Protos.MessageReponse> __Method_UpdateSubject = new grpc::Method<global::AttendanceFe.Protos.SubjectRequestUpdate, global::AttendanceFe.Protos.MessageReponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateSubject",
        __Marshaller_subject_SubjectRequestUpdate,
        __Marshaller_subject_MessageReponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::AttendanceFe.Protos.SubjectRequest, global::AttendanceFe.Protos.MessageReponse> __Method_DeleteSubject = new grpc::Method<global::AttendanceFe.Protos.SubjectRequest, global::AttendanceFe.Protos.MessageReponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeleteSubject",
        __Marshaller_subject_SubjectRequest,
        __Marshaller_subject_MessageReponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::AttendanceFe.Protos.Empty, global::AttendanceFe.Protos.SubjectListResponse> __Method_GetAllSubjects = new grpc::Method<global::AttendanceFe.Protos.Empty, global::AttendanceFe.Protos.SubjectListResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllSubjects",
        __Marshaller_subject_Empty,
        __Marshaller_subject_SubjectListResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::AttendanceFe.Protos.SubjectReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for SubjectRPC</summary>
    public partial class SubjectRPCClient : grpc::ClientBase<SubjectRPCClient>
    {
      /// <summary>Creates a new client for SubjectRPC</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public SubjectRPCClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for SubjectRPC that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public SubjectRPCClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected SubjectRPCClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected SubjectRPCClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.SubjectResponse GetSubjects(global::AttendanceFe.Protos.SubjectRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSubjects(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.SubjectResponse GetSubjects(global::AttendanceFe.Protos.SubjectRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSubjects, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.SubjectResponse> GetSubjectsAsync(global::AttendanceFe.Protos.SubjectRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSubjectsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.SubjectResponse> GetSubjectsAsync(global::AttendanceFe.Protos.SubjectRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSubjects, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.MessageReponse CreateSubject(global::AttendanceFe.Protos.SubjectRequestAdd request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSubject(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.MessageReponse CreateSubject(global::AttendanceFe.Protos.SubjectRequestAdd request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateSubject, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.MessageReponse> CreateSubjectAsync(global::AttendanceFe.Protos.SubjectRequestAdd request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSubjectAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.MessageReponse> CreateSubjectAsync(global::AttendanceFe.Protos.SubjectRequestAdd request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateSubject, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.MessageReponse UpdateSubject(global::AttendanceFe.Protos.SubjectRequestUpdate request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UpdateSubject(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.MessageReponse UpdateSubject(global::AttendanceFe.Protos.SubjectRequestUpdate request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UpdateSubject, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.MessageReponse> UpdateSubjectAsync(global::AttendanceFe.Protos.SubjectRequestUpdate request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UpdateSubjectAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.MessageReponse> UpdateSubjectAsync(global::AttendanceFe.Protos.SubjectRequestUpdate request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UpdateSubject, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.MessageReponse DeleteSubject(global::AttendanceFe.Protos.SubjectRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteSubject(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.MessageReponse DeleteSubject(global::AttendanceFe.Protos.SubjectRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeleteSubject, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.MessageReponse> DeleteSubjectAsync(global::AttendanceFe.Protos.SubjectRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteSubjectAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.MessageReponse> DeleteSubjectAsync(global::AttendanceFe.Protos.SubjectRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeleteSubject, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.SubjectListResponse GetAllSubjects(global::AttendanceFe.Protos.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllSubjects(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::AttendanceFe.Protos.SubjectListResponse GetAllSubjects(global::AttendanceFe.Protos.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllSubjects, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.SubjectListResponse> GetAllSubjectsAsync(global::AttendanceFe.Protos.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllSubjectsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::AttendanceFe.Protos.SubjectListResponse> GetAllSubjectsAsync(global::AttendanceFe.Protos.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllSubjects, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override SubjectRPCClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new SubjectRPCClient(configuration);
      }
    }

  }
}
#endregion
