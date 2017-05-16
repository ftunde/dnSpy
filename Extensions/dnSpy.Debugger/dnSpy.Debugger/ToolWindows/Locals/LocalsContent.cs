﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel.Composition;
using System.Linq;
using dnSpy.Contracts.Controls;
using dnSpy.Contracts.Debugger.CallStack;
using dnSpy.Contracts.Debugger.Evaluation;
using dnSpy.Contracts.Text;
using dnSpy.Contracts.Text.Classification;
using dnSpy.Debugger.Evaluation.UI;
using dnSpy.Debugger.Evaluation.ViewModel;

namespace dnSpy.Debugger.ToolWindows.Locals {
	[Export(typeof(LocalsContent))]
	sealed class LocalsContent : VariablesWindowContentBase {
		public static readonly Guid VariablesWindowGuid = new Guid("1A53B7B7-19AE-490F-9D67-F1992D849150");

		[ImportingConstructor]
		LocalsContent(IWpfCommandService wpfCommandService, VariablesWindowVMFactory variablesWindowVMFactory)
			: base(wpfCommandService, variablesWindowVMFactory) {
		}

		sealed class VariablesWindowValueNodesProviderImpl : VariablesWindowValueNodesProvider {
			public override DbgValueNodeInfo[] GetNodes(DbgLanguage language, DbgStackFrame frame) =>
				language.LocalsProvider.GetNodes(frame).Select(a => new DbgValueNodeInfo(a)).ToArray();
		}

		protected override VariablesWindowVMOptions CreateVariablesWindowVMOptions() {
			var options = new VariablesWindowVMOptions() {
				VariablesWindowValueNodesProvider = new VariablesWindowValueNodesProviderImpl(),
				WindowContentType = ContentTypes.LocalsWindow,
				NameColumnName = PredefinedTextClassifierTags.LocalsWindowName,
				ValueColumnName = PredefinedTextClassifierTags.LocalsWindowValue,
				TypeColumnName = PredefinedTextClassifierTags.LocalsWindowType,
				VariablesWindowKind = VariablesWindowKind.Locals,
				VariablesWindowGuid = VariablesWindowGuid,
			};
			return options;
		}
	}
}