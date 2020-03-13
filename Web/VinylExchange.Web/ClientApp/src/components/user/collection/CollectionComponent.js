import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight, faTimes } from "@fortawesome/free-solid-svg-icons";
import PlayerLoaderButton from "./../../common/PlayerLoaderButton";
import GoToReleaseButton from "./../../common/GoToReleaseButton";
import "./Collection.css";
import AddSalePopupContainer from "./../../common/popupForms/addSalePopupForm/AddSalePopupContainer";
import GradeBadge from "./../../common/badges/GradeBadge";

function CollectionCoponent(props) {
  return (
    <div className="col-lg-4 col-md-6 col-sm-8 col-xs-12">
      <div
        className="row border text-center justify-content-center"
        style={{ padding: "30px", height: "600px" }}
      >
         <div className="col-12 ">
        <img class="img-thumbnail" src={props.data.coverArt} height="150px" width="150px" />
        </div>
        <div>
          <br />
          <div className="col-12 border mb-10">
            <h6>Vinyl:</h6>
          </div>
          <div className="col-12 mb-10">
            <GradeBadge data={{ grade: props.data.vinylGrade }} />
          </div>
          <div className="col-12 border mb-10">
            <h6>Sleeve:</h6>
          </div>
          <div className="col-12 mb-10">
            <GradeBadge data={{ grade: props.data.sleeveGrade }} />
          </div>

          <div className="col-12 text-center">
            <div
              class="btn-group"
              role="group"
              aria-label="Basic example"
            ></div>
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
                releaseId: props.data.releaseId,
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
