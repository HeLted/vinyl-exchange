import React,{Component} from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner"

function ReleaseInfoComponent (props){
   
  const component =  props.data.isLoading ? (<BorderSpinner/>) : (<ul>
      <li>
        <h5>
          <b>
            -Artist - <i>{props.data.release.artist}</i>
          </b>
        </h5>
      </li>
      <li>
        <h5>
          <b>
            -Title - <i>{props.data.release.title}</i>
          </b>
        </h5>
      </li>
      <li>
        <h5>
          <b>
            -Label - <i>{props.data.release.label}</i>
          </b>
        </h5>
      </li>
      <li>
        <h5>
          <b>
            -Year - <i>{props.data.release.year}</i>
          </b>
        </h5>
      </li>
      <li>
        <h5>
          <b>
            -Format - <i>{props.data.release.format}</i>
          </b>
        </h5>
      </li>
    </ul>);
   
   return(component);
}

export default ReleaseInfoComponent;