import React, { useEffect, useState } from "react";
import { getAccommodationById } from "../../actions/actionsAccommodation";
import { connect } from "react-redux";
import { compose } from "redux";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import BuyAccommodationModal from "./BuyAccommodationModal";

function ReviewAccommodation(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getAccommodationById(localStorage.getItem("accommodation-id"));
  }, []);

  const Accommodation = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    return (
      <img
        src={"data:image/jpg;base64," + props.loadedImage.fileContents}
        style={{ width: 600, height: 320 }}
        className="mb-3"
      />
    );
  };

  const displayModalPost = (item) => {
    debugger;
    if (item != undefined) {
      setItem(item);
    }
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    debugger;
    if (item !== undefined) {
      if (shoppingCartList.length > 0) {
        if (shoppingCartList[0].item.ownerId != item.ownerId) {
          toast.configure();
          toast.error("You can't add items of another owner to cart!", {
            position: toast.POSITION.TOP_RIGHT,
          });
        } else {
          setShowPostModal(!showPostModal);
        }
      } else {
        setShowPostModal(!showPostModal);
      }
    }
    debugger;
  };

  const edit = (item) => {
    localStorage.setItem("accommodation-id", item.id);
    window.location = "/edit-accommodation";
  };

  if (props.accommodation === undefined) {
    return null;
  }

  return (
    <React.Fragment>
      {showPostModal ? (
        <BuyAccommodationModal
          show={showPostModal}
          item={item}
          onShowChange={() => displayModalPost()}
        />
      ) : null}
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <Accommodation />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label className="label">Name:</label>
            <input
              type="text"
              name="name"
              class="form-control"
              id="name"
              value={props.accommodation.name}
              disabled={true}
              placeholder="Enter name of product"
            />
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Cost per night:</label>
            <input
              type="text"
              name="price"
              class="form-control"
              id="price"
              value={props.accommodation.costPerNight + " €"}
              disabled={true}
              placeholder="Enter price"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Description:</label>
            <textarea
              name="description"
              value={props.accommodation.description}
              cols="40"
              rows="5"
              class="form-control"
              disabled={true}
              placeholder="Enter promotion/action"
            ></textarea>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Address:</label>
            <input
              type="text"
              name="address"
              class="form-control"
              id="address"
              value={props.accommodation.address}
              disabled={true}
              placeholder="Enter address"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">City:</label>
            <input
              type="text"
              name="city"
              class="form-control"
              id="city"
              value={props.accommodation.city}
              disabled={true}
              placeholder="Enter city"
            />
          </div>
        </div>
      </div>
      {sessionStorage.getItem("roleWebShop") === "RegisteredUser" ? (
        <div style={{ textAlign: "center" }} className="mt-5 pb-5">
          <button
            onClick={() => displayModalPost(props.accommodation)}
            className="btn btn-primary"
          >
            Add to cart
          </button>
        </div>
      ) : sessionStorage.getItem("userIdWebShop") ===
        props.accommodation.ownerId ? (
        <div style={{ textAlign: "center" }} className="mt-5 pb-5">
          <button
            onClick={() => edit(props.accommodation)}
            className="btn btn-primary"
          >
            Edit accommodation
          </button>
        </div>
      ) : null}
    </React.Fragment>
  );
}

const mapStateToProps = (state) => ({
  accommodation: state.accommodation,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, {
    getAccommodationById,
  })
)(ReviewAccommodation);
