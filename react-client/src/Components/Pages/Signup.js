import React, { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { useSnackbar } from 'notistack'
import { useNavigate } from 'react-router-dom';
import { LithuanianCities } from '../Views/LithuanianCities.js';
import CitySelect from '../Views/CitySelect';

const cityOptions = []

export const Signup = (props) => {
    const navigate = useNavigate();

    for (let i = 0; i < LithuanianCities.length; ++i) {
        cityOptions[i] = { value: i, label: LithuanianCities[i] }
    }

    const [redirectionMsg, setRedirectionMsg] = useState("");
    const { enqueueSnackbar, closeSnackbar } = useSnackbar()

    const formik = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            password: "",
            city: ""
        },
        validationSchema: Yup.object({
            firstName: Yup
                        .string()
                        .max(30, "Name can only be up to 30 characters")
                        .matches(/^[a-zA-Z]+(([',. -][a-zA-Z])?[a-zA-Z]*)*$/, 'Only use alphabet letters')
                        .required("Required"),
            lastName: Yup
                        .string()
                        .max(30, "Lastname can only be up to 30 characters")
                        .matches(/^[a-zA-Z]+(([',. -][a-zA-Z])?[a-zA-Z]*)*$/, 'Only use alphabet letters')
                        .required("Required"),
            email: Yup
                        .string()
                        .email("Invalid email address")
                        .required("Required"),
            password: Yup
                        .string()
                        .min(6, "Password must be at least 6 characters long")
                        .max(20,"Password can only be up to 20 characters")
                        .required("Required"),
            city: Yup
                        .string()
                        .max(30, "City can only be up to 30 characters")
                        .required("Required")
        }),
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/registration',
                data: values
            }).then((response) => {

                // if account was created, redirect in 5 seconds
                if (response.data.isSuccess) {

                    enqueueSnackbar(response.data.message, {variant: "success"});

                    let i = 5;
                    setRedirectionMsg(`Redirecting in ${i}`);
                    let interval = setInterval(() => {
                        i--;
                        if (i < 0) {
                            clearInterval(interval);
                            navigate(`/`);
                        }
                        setRedirectionMsg(`Redirecting in ${i}`);
                    }, 1000);
                } else {
                    enqueueSnackbar(response.data.message, {variant: "error"});
                }
            })  
        }
    })

    return (
        <>
        <h1>Registration</h1>
        <div className="centered-container signup-container">
            <form onSubmit={formik.handleSubmit}>
            <div className="form-field-container-flex">
                        <div className="form-field-flex-text"><label htmlFor="firstName">First Name</label></div>
                    <div className="form-field-flex">
                            <input className="signup-form-input"
                            name="firstName" 
                            id="firstName" 
                            type="text" 
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.firstName}
                            />
                        { 
                            formik.touched.firstName && formik.errors.firstName &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.firstName}
                                </p> 
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                        <div className="form-field-flex-text"><label htmlFor="lastName">Last Name</label></div>
                    <div className="form-field-flex">
                            <input className="signup-form-input"
                            name="lastName" 
                            id="lastName" 
                            type="text"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.lastName}
                        />
                        { 
                            formik.touched.lastName && formik.errors.lastName &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.lastName}
                                </p> 
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                        <div className="form-field-flex-text"><label htmlFor="email">Email</label></div>
                    <div className="form-field-flex">
                            <input className="signup-form-input"
                            name="email" 
                            id="email" 
                            type="email"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.email}
                        />
                        { 
                            formik.touched.email && formik.errors.email &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.email}
                                </p> 
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                        <div className="form-field-flex-text"><label htmlFor="password">Password</label></div>
                    <div className="form-field-flex">
                            <input className="signup-form-input"
                            name="password" 
                            id="password" 
                            type="password"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.password}
                        />
                        { 
                            formik.touched.password && formik.errors.password &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.password}
                                </p> 
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex-text"><label htmlFor="city">City</label></div>
                    <div className="form-field-flex">
                        <CitySelect
                            options={cityOptions}
                            value={formik.values.city}
                            className={'number'}
                            onChange={value => formik.setFieldValue('city',value.label)}
                        />
                        { 
                            formik.touched.city && formik.errors.city &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.city}
                                </p> 
                        }
                    </div>
                </div>
                <button type="submit">Submit</button>
            </form>
            <p>{redirectionMsg}</p>
        </div>
        </>
    )
}