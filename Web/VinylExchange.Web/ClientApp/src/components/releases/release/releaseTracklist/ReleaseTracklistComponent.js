import React from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner"

function ReleaseTracklistComponent(props){

    const tracksRows = props.data.tracks.map((trackObj,index)=>{
        return (<tr key={trackObj.id}>
          <td>{atob(trackObj.fileName.split("@---@")[1].split(".")[0])}</td>
          </tr>)
    });

    const component = props.data.isLoading ? (<tr><td><BorderSpinner/></td></tr>) : (tracksRows)

   return( <table className="tracklist-table table-borderless border text-center">
   <tbody>
     {tracksRows.length ===  0 && props.data.isLoading === false ?  <tr><td>This release has no tracks</td></tr>: component }
   </tbody>
 </table>)
}

export default ReleaseTracklistComponent;