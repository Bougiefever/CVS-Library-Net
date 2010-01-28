namespace PServerClient
{
   /// <summary>
   ///  `command_options' that are available across several CVS commands. 
   /// These options are always given to the right of `cvs_command'. 
   /// Not all commands support all of these options; each option is only supported 
   /// for commands where it makes sense. However, when a command has one of these 
   /// options you can almost always count on the same behavior of the 
   /// option as in other commands.
   /// </summary>
   public enum CommandOption
   {
      /// <summary>
      /// -D date_spec 
      /// `-D' is available with the checkout, diff, export, history, rdiff, rtag, and update commands. 
      /// Use the most recent revision no later than date_spec. date_spec is a single argument, 
      /// a date description specifying a date in the past. The specification is sticky 
      /// when you use it to make a private copy of a source file; 
      /// that is, when you get a working file using `-D', CVS records the date you specified, 
      /// so that further updates in the same directory will use the same date. 
      /// (The history command uses this option in a slightly different way; see section history options). 
      /// A wide variety of date formats are supported by CVS. The most standard ones are ISO8601 
      /// (from the International Standards Organization) and the Internet e-mail standard 
      /// (specified in RFC822 as amended by RFC1123). 
      /// ISO8601 dates have many variants but a few examples are: 
      /// 1972-09-24
      /// 1972-09-24 20:05
      /// For more details about ISO8601 dates, see: 
      /// http://www.ft.uni-erlangen.de/~mskuhn/iso-time.html
      /// In addition to the dates allowed in Internet e-mail itself, 
      /// CVS also allows some of the fields to be omitted. For example: 
      /// 24 Sep 1972 20:05
      /// 24 Sep
      /// The date is interpreted as being in the local timezone, u
      /// nless a specific timezone is specified. These two date formats are preferred. 
      /// However, CVS currently accepts a wide variety of other date formats. 
      /// They are intentionally not documented here in any detail, and future versions 
      /// of CVS might not accept all of them. One such format is month/day/year. 
      /// This may confuse people who are accustomed to having the month and day in the 
      /// other order; `1/4/96' is January 4, not April 1. Remember to quote the argument to 
      /// the `-D' flag so that your shell doesn't interpret spaces as argument separators. A
      ///  command using the `-D' flag can look like this: 
      /// </summary>
      DateSpec = 0,

      /// <summary>
      /// -f 
      /// `-f' is available with these commands: annotate, checkout, export, rdiff, rtag, and update. 
      /// When you specify a particular date or tag to CVS commands, they normally ignore 
      /// files that do not contain the tag (or did not exist prior to the date) that you 
      /// specified. Use the `-f' option if you want files retrieved even when there is 
      /// no match for the tag or date. (The most recent revision of the file will be used). 
      /// Warning: The commit and remove commands also have a `-f' option, but it has a different 
      /// behavior for those commands.
      /// </summary>
      RetrieveEvenIfNoMatch = 1,

      /// <summary>
      /// -k kflag 
      /// Alter the default processing of keywords. Your kflag specification is sticky when you 
      /// use it to create a private copy of a source file; that is, when you use this option 
      /// with the checkout or update commands, CVS associates your selected kflag with the file, 
      /// and continues to use it with future update commands on the same file until you specify 
      /// otherwise. The `-k' option is available with the add, checkout, diff, import and 
      /// update commands. 
      /// </summary>
      KeywordProcessingFlag = 2,

      /// <summary>
      /// -l 
      /// Local; run only in current working directory, rather than recursing through subdirectories. 
      /// Warning: this is not the same as the overall `cvs -l' option, which you can specify to the 
      /// left of a cvs command! 
      /// Available with the following commands: annotate, checkout, commit, diff, edit, editors, 
      /// export, log, rdiff, remove, rtag, status, tag, unedit, update, watch, and watchers. 
      /// </summary>
      Local = 3,

      /// <summary>
      /// -m message 
      /// Use message as log information, instead of invoking an editor. Available with the 
      /// following commands: add, commit and import. 
      /// </summary>
      Message = 4,

      /// <summary>
      /// -n 
      /// Do not run any checkout/commit/tag program. (A program can be specified to run on 
      /// each of these activities, in the modules database; this option bypasses it). 
      /// Warning: this is not the same as the overall `cvs -n' option, which you can specify 
      /// to the left of a cvs command! 
      /// Available with the checkout, commit, export, and rtag commands. 
      /// </summary>
      DoNothing = 5,

      /// <summary>
      /// -P 
      /// Prune empty directories. 
      /// </summary>
      Prune = 6,

      /// <summary>
      /// -p 
      /// Pipe the files retrieved from the repository to standard output, rather than writing 
      /// them in the current directory. Available with the checkout and update commands. 
      /// </summary>
      Pipe = 7,

      /// <summary>
      /// -R 
      /// Process directories recursively. This is on by default. Available with the following 
      /// commands: annotate, checkout, commit, diff, edit, editors, export, rdiff, remove, 
      /// rtag, status, tag, unedit, update, watch, and watchers. 
      /// </summary>
      Recursive = 8,

      /// <summary>
      /// -r tag 
      /// Use the revision specified by the tag argument instead of the default head revision. 
      /// As well as arbitrary tags defined with the tag or rtag command, two special tags 
      /// are always available: `HEAD' refers to the most recent version available in the repository, 
      /// and `BASE' refers to the revision you last checked out into the current working directory. 
      /// The tag specification is sticky when you use this with checkout or update to make your own 
      /// copy of a file: CVS remembers the tag and continues to use it on future update commands, 
      /// until you specify otherwise. The tag can be either a symbolic or numeric tag. 
      /// Specifying the `-q' global option along with the `-r' command option is often useful, 
      /// to suppress the warning messages when the RCS file does not contain the specified tag. 
      /// Warning: this is not the same as the overall `cvs -r' option, which you can specify to 
      /// the left of a CVS command! 
      /// `-r' is available with the checkout, commit, diff, history, export, rdiff, 
      /// rtag, and update commands. 
      /// </summary>
      Revision = 9,

      /// <summary>
      /// -W 
      /// Specify file names that should be filtered. You can use this option repeatedly. 
      /// The spec can be a file name pattern of the same type that you can specify in the 
      /// `.cvswrappers' file. Available with the following commands: import, and update. 
      /// </summary>
      Filter = 10
   }
}