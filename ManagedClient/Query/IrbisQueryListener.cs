//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from IrbisQuery.g4 by ANTLR 4.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace ManagedClient.Query {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="IrbisQueryParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
[System.CLSCompliant(false)]
public interface IIrbisQueryListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>starOperator2</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStarOperator2([NotNull] IrbisQueryParser.StarOperator2Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>starOperator2</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStarOperator2([NotNull] IrbisQueryParser.StarOperator2Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>starOperator3</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStarOperator3([NotNull] IrbisQueryParser.StarOperator3Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>starOperator3</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStarOperator3([NotNull] IrbisQueryParser.StarOperator3Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>starOperator1</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStarOperator1([NotNull] IrbisQueryParser.StarOperator1Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>starOperator1</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStarOperator1([NotNull] IrbisQueryParser.StarOperator1Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>plusOperator3</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlusOperator3([NotNull] IrbisQueryParser.PlusOperator3Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>plusOperator3</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlusOperator3([NotNull] IrbisQueryParser.PlusOperator3Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>plusOperator2</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlusOperator2([NotNull] IrbisQueryParser.PlusOperator2Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>plusOperator2</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlusOperator2([NotNull] IrbisQueryParser.PlusOperator2Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>fOperator</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFOperator([NotNull] IrbisQueryParser.FOperatorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>fOperator</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFOperator([NotNull] IrbisQueryParser.FOperatorContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>parenOuter</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenOuter([NotNull] IrbisQueryParser.ParenOuterContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>parenOuter</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenOuter([NotNull] IrbisQueryParser.ParenOuterContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>gOperator</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGOperator([NotNull] IrbisQueryParser.GOperatorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>gOperator</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGOperator([NotNull] IrbisQueryParser.GOperatorContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>plusOperator1</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPlusOperator1([NotNull] IrbisQueryParser.PlusOperator1Context context);
	/// <summary>
	/// Exit a parse tree produced by the <c>plusOperator1</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPlusOperator1([NotNull] IrbisQueryParser.PlusOperator1Context context);

	/// <summary>
	/// Enter a parse tree produced by the <c>reference</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReference([NotNull] IrbisQueryParser.ReferenceContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>reference</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReference([NotNull] IrbisQueryParser.ReferenceContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="IrbisQueryParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] IrbisQueryParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="IrbisQueryParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] IrbisQueryParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>entry</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEntry([NotNull] IrbisQueryParser.EntryContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>entry</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEntry([NotNull] IrbisQueryParser.EntryContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>levelTwoOuter</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLevelTwoOuter([NotNull] IrbisQueryParser.LevelTwoOuterContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>levelTwoOuter</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelThree"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLevelTwoOuter([NotNull] IrbisQueryParser.LevelTwoOuterContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>dotOperator</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDotOperator([NotNull] IrbisQueryParser.DotOperatorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>dotOperator</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelOne"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDotOperator([NotNull] IrbisQueryParser.DotOperatorContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>levelOneOuter</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLevelOneOuter([NotNull] IrbisQueryParser.LevelOneOuterContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>levelOneOuter</c>
	/// labeled alternative in <see cref="IrbisQueryParser.levelTwo"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLevelOneOuter([NotNull] IrbisQueryParser.LevelOneOuterContext context);
}
} // namespace ManagedClient.Search