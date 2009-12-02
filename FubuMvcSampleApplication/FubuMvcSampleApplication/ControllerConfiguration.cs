using FubuMVC.Container.StructureMap.Config;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Conventions.ControllerActions;
using FubuMvcSampleApplication.Controllers;
using FubuMvcSampleApplication.Web;

namespace FubuMvcSampleApplication
{
    public class ControllerConfiguration
    {
        public static void Configure()
        {
            ControllerConfig.Configure = x =>
                                             {
                                                 // Action conventions
                                                 x.ActionConventions(convention =>
                                                                         {
                                                                             convention.Add<wire_up_JSON_URL>();
                                                                             convention.Add<wire_up_404_handler_URL>();
                                                                         });

                                                 x.ByDefault.EveryControllerAction(action =>
                                                                                       {
                                                                                           // Default behaviour for all actions
                                                                                           action.Will
                                                                                               <execute_the_result>();
                                                                                           // add a behaviour to output as Json if it is a json request
                                                                                           action.Will
                                                                                               <
                                                                                                   output_as_json_if_requested
                                                                                                   >();
                                                                                       });

                                                 //Automatic controller registration
                                                 x.AddControllerActions(
                                                     assembly => assembly.UsingTypesInTheSameAssemblyAs<ViewModel>(
                                                                     types =>
                                                                     types.SelectTypes(
                                                                         type => type.Namespace.EndsWith("Controllers")
                                                                                 && type.Name.EndsWith("Controller"))));

                                                 // Override default behaviours defined above
                                                 x.OverrideConfigFor<UserController>(controller =>
                                                                                     controller.New(null),
                                                                                     configuration =>
                                                                                     configuration.UseViewFrom(
                                                                                         (UserController c) =>
                                                                                         c.Edit(null)));

                                                 x.OverrideConfigFor<UserController>(controller =>
                                                                                     controller.Delete(null),
                                                                                     configuration =>
                                                                                     configuration.UseViewFrom(
                                                                                         (UserController c) =>
                                                                                         c.Index(null)));

                                                 x.OverrideConfigFor<UserController>(controller =>
                                                                                     controller.Save(null),
                                                                                     configuration =>
                                                                                     configuration.AddBehavior
                                                                                         <OutputAsJson>());
                                                 x.OverrideConfigFor<UserController>(controller =>
                                                                                     controller.Index(null),
                                                                                     configuration =>
                                                                                     configuration.AddOtherUrl(
                                                                                         "user/List.aspx"));
                                             };
        }
    }
}