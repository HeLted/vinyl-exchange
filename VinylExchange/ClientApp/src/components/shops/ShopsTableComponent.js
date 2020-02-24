import React, { Fragment, Component } from "react";
import "./ShopsTable.css";
import InfiniteScroll from "react-infinite-scroller";
import { css } from "@emotion/core";
import PulseLoader from "react-spinners/PulseLoader";
import PlayerLoaderButton from "./../common/PlayerLoaderButton";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMeh } from "@fortawesome/free-solid-svg-icons";

class ShopsTableComponent extends Component {
  constructor() {
    super();
    this.state = { loadEjectButtons: [] };
  }

  render() {
    const shops = this.props.data.shops.map(shop => {
      const mainPhotoImageSource =
        shop.mainPhoto !== null
          ? "/file/media" + shop.mainPhoto.path + shop.mainPhoto.fileName
          : "https://cdn4.iconfinder.com/data/icons/ui-beast-4/32/Ui-12-512.png";

      return (
        <Fragment key={shop.id}>
          {/* <tr
            onClick={() => this.props.functions.handleRedirectToshop(shop.id)}
          >
            <td>
              <img src={mainPhotoimageSource} width="80" height="80" alt="" />
            </td>
            <td>{shop.name}</td>

            <td>
              {shop.country} - {shop.town}
            </td>
          </tr> */}
          <li class="list-inline-item" >
            <div class="card" style={{ width: "18rem" }}>
              <img
                class="card-img-top"
                src={mainPhotoImageSource}
               height="250px"
                alt="Card image cap"
              />
              <div class="card-body">
                <h5 class="card-title">{shop.name}</h5>
                <p class="card-text">
                  Some quick example text to build on the card title and make up
                  the bulk of the card's content.
                </p>
                <a href="#" class="btn btn-primary" />
              </div>
            </div>
          </li>
        </Fragment>
      );
    });

    const loader = (
      <tr key="shop loader">
        <td>
          <PulseLoader
            size={15}
            //size={"150px"} this also works
            color={"#13eddb"}
            loading={this.props.data.isLoadMoreshopsLoading}
          />
        </td>
      </tr>
    );

    return (
      <div className="shop-div">
        <InfiniteScroll
          initialLoad={false}
          hasMore={this.props.data.isThereMoreShopsToLoad}
          loadMore={() => this.props.functions.handleLoadShops(false)}
          loader={loader}
          element={"div"}
          useWindow={false}
          getScrollParent={() => this.scrollParentRef}
        >
          <ul className="list-inline">{shops}</ul>
        </InfiniteScroll>

        {(this.props.data.isThereMoreshopsToLoad === false &&
          shops.length > 6) ||
        (shops.length === 0 &&
          this.props.data.isLoadMoreshopsLoading === false) ? (
          <tbody>
            <tr>
              <td colSpan="3">
                <div className="search-not-found">
                  <h6>
                    <b>
                      <i>
                        <FontAwesomeIcon icon={faMeh} /> No More shops.Maybe Try
                        Another Search Term.
                      </i>
                    </b>
                  </h6>
                </div>
              </td>
            </tr>
          </tbody>
        ) : null}
      </div>
    );
  }
}

export default ShopsTableComponent;
