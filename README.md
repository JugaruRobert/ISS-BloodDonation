# BloodDonation

Blood donation

Description
Annually, over 600.000 people need blood or blood preparations. Unfortunately, many times the patients are faced with blood shortage and thus surgery and treatment are postponed.

Romania continues to stay at the end of Europe’s blood donation list, with only 1.7% of the adult population donating blood. With that in mind if the need was great Romanians did form long queues to give blood. The need for blood remains a constant one, even after the situation has passed. Storing large quantities of blood from people doing it once or twice is not a solution since the shelf life of blood is short. Thrombocytes can be used for up to 5 days, red blood cells for 42 days, while blood plasma while frozen can be stored up 12 months.

Regular donations are vital for the transfusion system, and for people who need safe, plentiful and on time blood products from responsible donors. As a side note some patients may only get blood products from people who donate 2-3 times a year. (www.pentruviata.com)

Donation may be done by anyone who satisfies certain criteria (http://www.ctsbucuresti.ro/) which are the following:
Any Romanian citizen or EU citizen living in Romania, in good health
Age between 18-60
Weight above 50Kg
Stable pulse, 60-100 beats/minute
Systolic blood pressure between 100 and 180 mmHg
No surgeries in the last 6 months
Women must not be: pregnant, post birth, or menstruating
No alcohol or fat consumption in the last 48 hours
Must not be in treatment for: hypertension, heart disease, kidney disease, mental illness, blood disease, or endocrine affections

The donor must not have or have had the following diseases:
Hepatitis
hepatitis (of any type)
TB
Pox
Malaria
Epilepsy and other neurological diseases
Mental illness
Brucellosis
Ulcer
Diabetes
Heart diseases
Skin diseases: psoriasis, vitiligo
Myopia over (-) 6 diopters
Cancer
 
 
Before donating:
The morning before blood donation you may drink a cup of tea or coffee, and eat a light breakfast of fruits and vegetables
Do not smoke and hour before and after blood donation
Arrive well slept, and not after a night of working or staying up
 
Journey of a blood container:

1.    Sampling
Each donor must complete a form of his/her lifestyle and medical history. They will next go to a doctor who will decide the prospective donors aptitude for donation. If they are deemed qualified for donation a certified medical staff will harvest the blood.

2.    Preparation
Each harvested container will go through a process of filtration, centrifuge, and separation to obtain the components of blood: red blood cells, plasma, and thrombocytes. If a patient need only one component then they will receive only that part.

3.    Biological quality control
Each sample is tested on two fronts: Immuno-Hematology and blood transmissible diseases (HIV / AIDS, hepatitis B, hepatitis C, syphilis, HTLV, ALT). If any sample fails a test it is marked unusable and the donor is privately notified.

4.    Redistribution
The containers are redistributed among the hospitals and clinics that need them with each part having the following shelf life:
Thrombocytes – 5 days
Red blood cells – 42 days
Plasma – several months
 
Avoiding transfusion accidents

To avoid transfusion accidents, before any transfusion the patient is tested for compatibility (potential conflicts between the antibodies of the patient and the antigens of the donor). Finally, only the compatible units are used for treatment. It is mandatory to have the blood group and Rh of the patient before performing the transfusion.
 


Objectives
1.    Application
Offer a fast and easy way for anyone to register for blood donation and check if they are eligible for said donation.
Offer the possibility for doctors to request blood and to check the status of their request
Offer easy communication between doctors in the hospital, the transfusion center, and the donors
Offer donors easy access to their results and encourage repeated donation

2.    Laboratory objectives
The first objective is to properly understand the concepts learnt at the course. In this context the focus will be validating the requirements and using modeling through the project life cycle. Software engineering give you techniques that support a predictable, on time, and on budget software development.

Using models should aid in a better understanding of the problem being solved, and the proposed solutions. Strive to automate large parts of the work using various frameworks, for example the database should be created automatically from code using frameworks like Hibernate or Entity Framework. Do not ignore testing, for it is one of the most expensive part of the application.

Due to the complexity of the application teamwork is necessary. Students will form teams of 6-8 depending on group size. Each team will have a team leader who will be responsible for the project plan, task distribution and redistribution in case a team member is unable to perform. In the end it’s the team leader who proposes grades for each team member. Each team must be able to access the team repository, being able to see the status of the project, be able to download it, and be able to upload their work. Each team member is responsible to know what their colleagues have been working on, how they solved problems etc. It is not acceptable to divide work along the lines of “this person will do all testing/documentation/etc“.


The application must use a multi-tier architecture: frontend, business logic, and backend. Besides a functional application each team must present the following:
Functional model by using sequence diagrams of all use cases
Analytic model (class diagrams)
Design model (class diagrams including framework elements)
Testing model
Usage model
Plans of execution including changes through time
PowerPoint presentation
 
Functional requirements
The application must support the following types of users:

Donors, they can register using the form below. They may view their blood analysis history and when is the next time they may go again. They may change their personal details, and donation related information from part 1. If they are donating for a specific person they must fill in specific fields.


Doctors, will fill out forms for what kind of blood they need, they will see the status of their requests as well as if there were enough donations for a specific person.

Donation center personnel, will do donor management, they will collect donor data as well as manage the journey of the blood container. The software will help determine the closest compatible donor who is able to give blood again and helps contact them.

Once harvested the journey of the container must be visible and editable at each step. Once all the tests on the samples have been made the results must be visible to the donor. At any moment transfusion center personnel and doctors in a given area must be able to see the available stocks.

If a patient need blood a doctor will fill out a request, containing group, urgency level (high/medium/low), and location. The request is processed by the donation center personnel, who if they do not have what is needed will contact the nearest(geographically) compatible donor who had enough time passed since last donation. Requests are handled by priority unless the patient is an active donor, then they will receive priority.

If the volunteer wants to donate to a specific person they must mention their name. The doctor of the patient can view at any moment if enough people have donated for that patient.
The software must allow editing relevant information at each step of the container’s journey, but only up to that point, for example you cannot increase the stock of a harvested component for transfusion until the tests are done on them.


 

