using System;
using System.Collections.Generic;
using System.Linq;
using Confuser.Core;
using Confuser.Core.Helpers;
using Confuser.Core.Services;
using Confuser.Renamer;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Confuser.Protections
{
	[BeforeProtection(new string[]
	{
		"Ki.ControlFlow",
		"Ki.Constants",
		"Ki.ForceElevation"
	})]
	internal class AntiHttpDebugger : Protection
	{
		public override string Name
		{
			get
			{
				return "Anti Http Debugger Protection";
			}
		}

		public override string Description
		{
			get
			{
				return "This protection will remove the program if the user have http debugger on his computer.";
			}
		}

		public override string Id
		{
			get
			{
				return "anti http debugger";
			}
		}

		public override string FullId
		{
			get
			{
				return "Ki.HttpDebugger";
			}
		}
		public override ProtectionPreset Preset
		{
			get
			{
				return ProtectionPreset.Maximum;
			}
		}

		protected override void Initialize(ConfuserContext context)
		{
		}

		protected override void PopulatePipeline(ProtectionPipeline pipeline)
		{
			pipeline.InsertPreStage(PipelineStage.ProcessModule, new AntiHttpDebugger.AntiHttpDebuggerPhase(this));
		}

		public const string _Id = "anti http debugger";
		public const string _FullId = "Ki.HttpDebugger";

		private class AntiHttpDebuggerPhase : ProtectionPhase
		{
			public AntiHttpDebuggerPhase(AntiHttpDebugger parent) : base(parent)
			{
			}

			public override ProtectionTargets Targets
			{
				get
				{
					return ProtectionTargets.Modules;
				}
			}

			public override string Name
			{
				get
				{
					return "Anti Http Debugger";
				}
			}

			protected override void Execute(ConfuserContext context, ProtectionParameters parameters)
			{
				TypeDef runtimeType = context.Registry.GetService<IRuntimeService>().GetRuntimeType("Confuser.Runtime.AntiHttpDebugger");
				IMarkerService service = context.Registry.GetService<IMarkerService>();
				INameService service2 = context.Registry.GetService<INameService>();
				foreach (ModuleDef moduleDef in parameters.Targets.OfType<ModuleDef>())
				{
					IEnumerable<IDnlibDef> enumerable = InjectHelper.Inject(runtimeType, moduleDef.GlobalType, moduleDef);
					MethodDef methodDef = moduleDef.GlobalType.FindStaticConstructor();
					MethodDef method2 = (MethodDef)enumerable.Single((IDnlibDef method) => method.Name == "Init");
					methodDef.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, method2));
					foreach (IDnlibDef def in enumerable)
					{
						service2.MarkHelper(def, service, (Protection)base.Parent);
					}
				}
			}
		}
	}
}
