import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight, faTimes } from "@fortawesome/free-solid-svg-icons";
import PlayerLoaderButton from "./../../common/PlayerLoaderButton";
import GoToReleaseButton from "./../../common/GoToReleaseButton";
import AddSalePopupFormContainer from "./../../common/popupForms/addSalePopupForm/AddSalePopupContainer";
import "./Collection.css";
import AddSalePopupContainer from "./../../common/popupForms/addSalePopupForm/AddSalePopupContainer";

function CollectionCoponent(props) {
  return (
    <div className="col-4">
      <div className="row border text-center justify-content-center" style={{padding:"30px",height:"500px"}}>
      
        <img
          className="card-img-top"
          src={props.data.coverArt}
          alt="Card image cap"
        />
        <div >
       <br/>
          <div className="col-12 border">
          <h6>
              Vinyl:
              <span className="grade-badge badge badge-primary">
                {props.data.vinylGrade}
              </span>
            </h6>
            </div>
            <br/>
            <div className="col-12 border">
            <h6>
      
             
              Sleeve:
              <span className="grade-badge badge badge-primary">
                {props.data.sleeveGrade}
              </span>
            </h6>
            </div>
         <br/>
          <div className="col-12 text-center">
            <div class="btn-group" role="group" aria-label="Basic example"></div>
            <button
              type="button"
              className="btn-spr btn btn-danger"
              onClick={() =>
                props.functions.handleRemoveFromCollection(props.data.id)
              }
            >
              <FontAwesomeIcon icon={faTimes} />
            </button>

            <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />
            <AddSalePopupContainer
              data={{
                collectionItemId: props.data.id,
                releaseId : props.data.releaseId,
                vinylGrade: props.data.vinylGrade,
                sleeveGrade: props.data.sleeveGrade,
                description: props.data.description
              }}
            />
            <GoToReleaseButton data={{ releaseId: props.data.releaseId }} />
          
          </div>
        </div>
      </div>
   
    </div>
  );
}

export default CollectionCoponent;
