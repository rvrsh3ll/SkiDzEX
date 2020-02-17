using System;
using System.Linq;
using Confuser.Core;
using dnlib.DotNet;

namespace Confuser.Protections {
	internal class AntiILDasmProtection : Protection {
		public const string _Id = "anti ildasm";
		public const string _FullId = "Ki.AntiILDasm";

		public override string Name {
			get { return "Anti IL Dasm Protection"; }
		}

		public override string Description {
			get { return "This protection marks the module with a attribute that discourage ILDasm from disassembling it."; }
		}

		public override string Id {
			get { return _Id; }
		}

		public override string FullId {
			get { return _FullId; }
		}

		public override ProtectionPreset Preset {
			get { return ProtectionPreset.Minimum; }
		}

		protected override void Initialize(ConfuserContext context) {
			//
		}

		protected override void PopulatePipeline(ProtectionPipeline pipeline) {
			pipeline.InsertPreStage(PipelineStage.ProcessModule, new AntiILDasmPhase(this));
		}

		class AntiILDasmPhase : ProtectionPhase {
			public AntiILDasmPhase(AntiILDasmProtection parent)
				: base(parent) { }

			public override ProtectionTargets Targets {
				get { return ProtectionTargets.Modules; }
			}

			public override string Name {
				get { return "Anti-ILDasm marking"; }
			}

			protected override void Execute(ConfuserContext context, ProtectionParameters parameters) {
				foreach (ModuleDef module in parameters.Targets.OfType<ModuleDef>()) {
					TypeRef attrRef = module.CorLibTypes.GetTypeRef("", "Abarcy");
					var ctorRef = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRef);

					TypeRef attrRefx = module.CorLibTypes.GetTypeRef("", "AbarcyᅠProtector");
					var ctorRefx = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRefx);

					TypeRef attrRefxx = module.CorLibTypes.GetTypeRef("", "ᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠ");
					var ctorRefxx = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRefxx);

					TypeRef attrRefxxx = module.CorLibTypes.GetTypeRef("", "ᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠ");
					var ctorRefxxx = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRefxxx);

					TypeRef attrRefxxxx = module.CorLibTypes.GetTypeRef("", "AbarcyᅠObfuscator");
					var ctorRefxxxx = new MemberRefUser(module, ".ctor", MethodSig.CreateInstance(module.CorLibTypes.Void), attrRefxxxx);

					var attr = new CustomAttribute(ctorRef);
					var attrx = new CustomAttribute(ctorRefx);
					var attrxx = new CustomAttribute(ctorRefxx);
					var attrxxx = new CustomAttribute(ctorRefxxx);
					var attrxxxx = new CustomAttribute(ctorRefxxxx);
					module.CustomAttributes.Add(attr);
					module.CustomAttributes.Add(attrx);
					module.CustomAttributes.Add(attrxx);
					module.CustomAttributes.Add(attrxxx);
					module.CustomAttributes.Add(attrxxxx);

				}
			}
		}
	}
}