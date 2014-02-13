using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ICSharpCode.NRefactory.Documentation;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using Monodoc;

namespace MonoDevelop.Projects
{
    public static class HelpService
    {
        static RootTree helpTree;
        static bool helpTreeInitialized;
        static object helpTreeLock = new object();
        static HashSet<string> sources = new HashSet<string>();

        /// <summary>
        /// Starts loading the MonoDoc tree in the background.
        /// </summary>
        public static void AsyncInitialize()
        {
            lock (helpTreeLock)
            {
                if (helpTreeInitialized)
                    return;
            }
            ThreadPool.QueueUserWorkItem(delegate
                {
                    // Load the help tree asynchronously. Reduces startup time.
                    InitializeHelpTree();
                });
        }

        //FIXME: allow adding sources without restart when extension installed (will need to be async)
        // will also be tricky we cause we'll also have update any running MonoDoc viewer
        static void InitializeHelpTree()
        {
            lock (helpTreeLock)
            {
                if (helpTreeInitialized)
                    return;

                //Counters.HelpServiceInitialization.BeginTiming();

                try
                {

                    //foreach (var node in AddinManager.GetExtensionNodes("/MonoDevelop/ProjectModel/MonoDocSources"))
                    //    sources.Add(((MonoDocSourceNode)node).Directory);
                    sources.Add("/usr/lib/monodoc");
                    sources.Add("/Library/Frameworks/Mono.framework/Versions/Current/lib/monodoc/");

                    //remove nonexistent sources
                    foreach (var s in sources.ToList().Where(d => !Directory.Exists(d)))
                        sources.Remove(s);

                    //foreach (var s in sources)
                    //    helpTree.AddSource(s);
                    helpTree = RootTree.LoadTree(sources.First());

                }
                catch (Exception ex)
                {
                    if (!(ex is ThreadAbortException) && !(ex.InnerException is ThreadAbortException))
                        //LoggingService.LogError("Monodoc documentation tree could not be loaded.", ex);
                        Console.WriteLine("Monodoc documentation tree could not be loaded." + ex);

                }
                finally
                {
                    helpTreeInitialized = true;
                    //Counters.HelpServiceInitialization.EndTiming();
                }
            }
        }

        /// <summary>
        /// A MonoDoc docs tree.
        /// </summary>
        /// <remarks>
        /// The tree is background-loaded the help service, and accessing the property will block until it is finished 
        /// loading. If you don't wish to block, check the <see cref="TreeInitialized"/> property first.
        //  </remarks>
        public static RootTree HelpTree
        {
            get
            {
                lock (helpTreeLock)
                {
                    if (!helpTreeInitialized)
                        InitializeHelpTree();
                    return helpTree;
                }
            }
        }

        /// <summary>
        /// Whether the MonoDoc docs tree has finished loading.
        /// </summary>
        public static bool TreeInitialized
        {
            get
            {
                return helpTreeInitialized;
            }
        }

        public static IEnumerable<string> Sources
        {
            get { return sources; }
        }

        //note: this method is very careful to check that the generated URLs exist in MonoDoc
        //because if we send nonexistent URLS to MonoDoc, it shows empty pages
        public static string GetMonoDocHelpUrl(ResolveResult result)
        {
            if (result == null)
                return null;

            //			if (result is AggregatedResolveResult) 
            //				result = ((AggregatedResolveResult)result).PrimaryResult;


            if (result is NamespaceResolveResult)
            {
                string namespc = ((NamespaceResolveResult)result).NamespaceName;
                //verify that the namespace exists in the help tree
                //FIXME: GetHelpXml doesn't seem to work for namespaces, so forced to do full render
                Monodoc.Node dummy;
#pragma warning disable 612,618
                if (!String.IsNullOrEmpty(namespc) && HelpTree != null && HelpTree.RenderUrl("N:" + namespc, out dummy) != null)
#pragma warning restore 612,618
                    return "N:" + namespc;
                else
                    return null;
            }

            IMember member = null;
            //			if (result is MethodGroupResolveResult)
            //				member = ((MethodGroupResolveResult)result).Methods.FirstOrDefault ();
            //			else 
            if (result is MemberResolveResult)
                member = ((MemberResolveResult)result).Member;

            if (member != null && member.GetMonodocDocumentation() != null)
                return member.GetIdString();

            var type = result.Type;
            if (type != null && !String.IsNullOrEmpty(type.FullName))
            {
                string t = "T:" + type.FullName;
#pragma warning disable 612,618
                if (HelpTree != null && HelpTree.GetHelpXml(t) != null)
#pragma warning restore 612,618
                    return t;
            }

            return null;
        }
    }
}
