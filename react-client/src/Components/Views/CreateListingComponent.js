import { Component } from 'react';
import React from 'react';
import { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import CustomRoommates from './CustomRoommates'
import "yup-phone";
import { LithuanianCities } from './LithuanianCities.js';
import CitySelect from './CitySelect';

const options = [
    { value: '1', label: '1' },
    { value: '2', label: '2' },
    { value: '3', label: '3+' },
]

const cityOptions = []

export const CreateListingComponent = (props) => {

    for(let i = 0; i < LithuanianCities.length; ++i){
        cityOptions[i] = {value: i, label: LithuanianCities[i]}
    }

    const [responseMessage, setResponseMessage] = useState("");

    const today = new Date();
    const date = new Intl.DateTimeFormat('en-EU', {year: 'numeric', month: '2-digit',day: '2-digit'}).format(today);

    const formik = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            phone: "",
            city: "",
            maxPrice: "",
            roommateCount: "1",
            extraComment: "",
            date: date,
        },
        validationSchema: Yup.object({
            firstName: Yup
                .string()
                .max(64, "Name can only be up to 64 characters")
                .required("Required"),
            lastName: Yup
                .string()
                .max(128, "Lastname can only be up to 128 characters")
                .required("Required"),
            phone: Yup
                .string()
                .phone("LT")
                .required("Required"),
            email: Yup
                .string()
                .email("Invalid email address")
                .required("Required"),
            city: Yup
                .string()
                .max(30, "City can only be up to 30 characters")
                .required("Required"),
            maxPrice: Yup
                .string()
                .max(20, "Maximum price can only be up to 20 characters")
                .matches(/^[0-9]+$/, 'Only use digits')
                .required("Required"),
            extraComment: Yup
                .string()
                .max(200, "Extra Comment can only be up to 200 characters")
        }),
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/listing',
                data: values
            }).then((response) => {
                console.log(response.data);
                props.toggleCreateListing(false);
            })
            }
        }
    )

    return (
        <>
        <div className="centered-container create-listing-container">
            <form onSubmit={formik.handleSubmit}>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="firstName">First name</label></div>
                    <div className="form-field-flex">
                        <input
                            name="firstName"
                            id="firstName"
                            type="text"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.firstName}
                        />
                        {
                            formik.touched.firstName && formik.errors.firstName &&
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.firstName}
                                </p>
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="lastName">Last name</label></div>
                    <div className="form-field-flex">
                        <input
                            name="lastName"
                            id="lastName"
                            type="text"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.lastName}
                        />
                        {
                            formik.touched.lastName && formik.errors.lastName &&
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.lastName}
                                </p>
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="email">Email</label></div>
                    <div className="form-field-flex">
                        <input
                            name="email"
                            id="email"
                            type="email"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.email}
                        />
                        {
                            formik.touched.email && formik.errors.email && 
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.email}
                                </p>
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="phone">Phone Number</label></div>
                    <div className="form-field-flex">
                        <input
                            name="phone"
                            id="phone"
                            type="phone"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.phone}
                        />
                        {
                            formik.touched.phone && formik.errors.phone && 
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.phone}
                                </p>
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="city">City</label></div>
                    <div className="form-field-flex">
                        <CitySelect
                            options={cityOptions}
                            value={formik.values.city}
                            className={'number'}
                            onChange={value => formik.setFieldValue('city',value.label)}
                        />
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="roomateCount">Number of Roommates</label></div>
                    <div className="form-field-flex">
                        <CustomRoommates
                            options={options}
                            value={formik.values.roommateCount}
                            className={'number'}
                            onChange={value => formik.setFieldValue('roommateCount',value.value)}
                        />
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="price">Maximum Price:</label></div>
                        <div className="form-field-flex" style={{ height: "150px" }}>
                            <input
                                name="maxPrice"
                                id="maxPrice"
                                type="maxPrice"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.maxPrice}>
                            </input>
                            {
                                formik.touched.maxPrice && formik.errors.maxPrice &&
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.maxPrice}
                                </p>
                            }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="extraComment">Extra Comment * </label></div>
                    <div className="form-field-flex">
                        <input
                            name="extraComment"
                            id="extraComment"
                            type="extraComment"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.extraComment}
                        />
                        {
                            formik.touched.extraComment && formik.errors.extraComment &&
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.extraComment}
                                </p>
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="date">Date </label></div>
                    <div className="form-field-flex">
                        <input
                            readOnly
                            value={formik.values.date}
                        /> 
                    </div>
                </div>
                <button type="submit">Submit</button>
            </form>
            <small>   * - Optional Fields</small>
            <p>{responseMessage}</p>
        </div>
        </>
    )
}
 