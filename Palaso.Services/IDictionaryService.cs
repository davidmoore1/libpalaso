using System.ServiceModel;

namespace Palaso.DictionaryService.Client
{
	[ServiceContract]
	public interface IDictionaryService
	{
		/// <summary>
		/// Search the dictionary for an ordered list of entries that may be what the user is looking for.
		/// </summary>
		/// <param name="writingSystemId"></param>
		/// <param name="form">The form to search on.  May be used to match on lexeme form, citation form, variants, etc.,
		/// depending on how the implementing dictionary services application.</param>
		/// <param name="method">Controls how matching should happen</param>
		/// <param name="ids">The ids of the returned elements, for use in other calls.</param>
		/// <param name="forms">The headwords of the matched elements.</param>
		[OperationContract]
		void GetMatchingEntries(string writingSystemId, string form, FindMethods method, out string[] ids, out string[] forms);

		/// <summary>
		/// Get an HTML representation of the entry, suitable for concatenating
		/// with other stuff (or other entries). In other words, this will not
		/// come surrounded by an <html> tag
		/// </summary>
		/// <param name="entryId"></param>
		/// <returns></returns>
		[OperationContract]
		string GetHmtlForEntry(string entryId);

		/// <summary>
		/// Used to help the dictionary service app know when to quit
		/// </summary>
		/// <param name="clientProcessId"></param>
		[OperationContract]
		void RegisterClient(int clientProcessId);

		/// <summary>
		/// Used to help the dictionary service app know when to quit
		/// </summary>
		/// <param name="clientProcessId"></param>
		[OperationContract]
		void DeregisterClient(int clientProcessId);

		/// <summary>
		/// Cause a gui application to come to the front, focussed on this entry, read to edit
		/// </summary>
		/// <param name="entryId"></param>
		[OperationContract]
		void JumpToEntry(string entryId);

		/// <summary>
		/// Add a new entry to the lexicon
		/// </summary>
		/// <returns>the id that was assigned to the new entry</returns>
		[OperationContract]
		string AddEntry(string lexemeFormWritingSystemId, string lexemeForm,
			string definitionWritingSystemId, string definition,
			string exampleWritingSystemId, string example);

		/// <summary>
		/// this is useful for unit tests, to see if the app went where we asked
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		string GetCurrentUrl();

		[OperationContract]
		void ShowUIWithUrl(string url);

		/// <summary>
		/// mostly for unit testing
		/// </summary>
		[OperationContract]
		bool IsInServerMode();

//        /// <summary>
//        /// Given an array of ids, get an array of forms to show
//        /// </summary>
//        /// <param name="writingSytemId">The writing system you want the form in</param>
//        /// <param name="ids"></param>
//        /// <returns></returns>
//        [OperationContract]
//        string[] GetFormsFromIds(string writingSytemId, string[] ids);

//todo        void AddInflectionalVariant(string writingSystemId, string variant);

	}


	public enum FindMethods
	{
		Exact,
		DefaultApproximate
	}
//    public enum ArticleCompositionFlags
//    {
//        Simple = 3,
//        Definition = 1,
//        Example = 2,
//        Synonyms = 4,
//        Antonyms = 8,
//        RelatedByDomain = 16,
//        Everything = 255
//    } ;
}