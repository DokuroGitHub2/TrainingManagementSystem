# Training management system

///TODO: when query just select necessary fields.
///TODO: rename property to correct grammar
///TODO: remove unused fields
///TODO: refactor source code
///TODO: test all endpoint and write test.
// TODO: check and add validation if not has
// TODO: HANDLE ALL error case response message.

```console
   Add-migration -Context ApplicationDbContext
   Update-Database -Context ApplicationDbContext
```

## Class _Nhon_

1. Creation
   1. Allow for easy creation of classes.
   2. Allow trainers or administrators to add details such as class name, description, and training plan.
   // **done**: Add author only role trainer allow use this endpoint.
2. Progress Tracking
   1. Have the ability to track progress and provide analytics on class performance.
   2. Allow trainers or administrators to view the progress of each student in the class through a dashboard or report. (**)

## Student

1. Progress Tracking
   1. Have the ability to track student progress through each training plan.
   2. Allow trainers and administrators to view the progress of each student through a dashboard or report.
   3. Have the ability to send automated notifications to students, trainers, or administrators when a training plan is completed or when a student is falling behind.// optional hangfire.
2. Performance Analytics
   1. Provide detailed analytics on student performance, including individual performance metrics and comparative metrics against the
   2. Allow for easy exporting of performance data for further analysis.
3. Profile Customization _Nhon_ ****done****
   1. Allow for custom fields to be added to student profiles to capture additional information that may be required.
   // add new table profile of student
   2. Allow students to customize their profiles and manage their personal information. _Nhon_
   // edit profile.
4. Communication (optional)
   1. Have the ability to send notifications to students regarding upcoming classes, training plans, and assessment deadlines.
   2. Have the ability to allow students to communicate with trainers or administrators through messaging or chat features.

## Class/Student

1. Enrollment Management
   1. Allow for easy enrollment of students into classes.
   // **DONE**: endpoint: POST /class/:id/enroll (id of class) body: {studentId: 1} return class and student.
   // post: add student to class. approve by admin. Create new table relation with user and class has fields(
      "classId": 1,
      "studentId": 1,
      "createdAt": "2021-09-30T08:00:00.000Z",
      "approveAt": "2021-09-30T08:00:00.000Z",
      "approveBy": 1,
   )
   2. Support retrieving the list of enrolled students for each class.
   // **done**: endpoint: GET /class/:id/enroll (id of class) return list student of class.
2. Absence Reporting
   1. Have the ability to track and manage class attendance.
         //TODO: endpoint:  get duration of training program for all program.
         // can not find duration property of training program.
   2. Allow trainers or administrators to view the list of reported absences for each student, or view summary on class.
         //**DONE**: endpoint: get list absence of student in class.
   3. Have the  to senabilityd automated notifications to students, trainers, or administrators when a student is absent from a class (optional).
         //TODO: allow send mails to student, trainer, admin when student absence.
3. Absence Approval
   1. Allow students to report their absences in advance, providing reasons and expected dates of return.
   // **DONE**: CRUD _**DONE**_
   2. Allow trainers or administrators to approve or reject reported absences.
   // **DONE**: endpoint: POST /attendance/:id/approve (id of student) _
   3. Allow trainers or administrators to mark student attendance and view attendance records.
   // **DONE**: endpoint: POST /attendance/:id/mark (id of student)

## Score

1. Tracking & Recording
   1. Have the ability to track student scores and provide analytics on student performance.
   // TODO: endpoint: GET /score/:id (id of student) return list score of student.
   2. Allow trainers or administrators to view the score records of each student and identify patterns of performance.
   3. Allow trainers or administrators to record and update student scores for each assessment.
   // TODO: endpoint: POST /score/:id (id of student) body: {score: 10, testId: 1} return score of student.
   // TODO: endpoint: PUT /score/:id (id of score) body: {score: 10} return score of student.
   // TODO: endpoint: DELETE /score/:id (id of score) return score of student.
   4. Support different types of scores, such as assignment, homework, final test, bonus.
2. Calculation & Report
   1. Have the ability to calculate final scores for each student based on the weighted _average_ of their scores in each assessment. (**done**)
   // TODO: endpoint: GET /score/:id (id of student) return score average of student.
   2. Support different weighting methods, such as equal weighting or custom weighting. (**done**)
   3. Allow trainers or administrators to generate score reports for each class or for individual students.
   4. Support filtering and sorting options for the score reports, such as by assessment type or score range.
   5. CRUD TestAssignment _Nhon_ **DONE**

## Trainer

1. Registration
   1. Allow trainers to register themselves by providing their basic information such as name, contact information, and credentials.
   // **done**: endpoint: POST /trainer {id}/ Role: {add role}
   // **done**: endpoint: POST /trainer body: {name: "Nhon", email: "nhon@gmail", password: "123456"} return trainer.
   2. Have the ability to authenticate and verify the identity and qualifications of the trainer.
   // **done**: endpoint: POST /trainer/login body: {email: "nhon@gmail", password: "123456"} return token.
   // **done**: endpoint: GET /trainer/:id (id of trainer) return trainer.
2. Assignment
   1. Have the ability to restrict trainer access to certain resources or assessments based on their qualifications or areas of expertise.
   // **done**
3. Evaluation
   1. Have the ability to evaluate the performance of trainers based on student feedback.
// TODO: endpoint: POST /student:trainerId/feedback
// create table feedback between student and syllabus.
// TODO: endpoint: GET /student:trainerId/feedback

//**DONE** super admin -> admin
//**DONE** admin -> trainer
//**DONE** GET PROFILE
//**done** admin -> class(program)
//**DONE** admin add trainer -> class (**)

// **done** trainer -> syllabus(unit(lesson(material)))
// **done** trainer -> Program(syllabus)
// **done** trainer -> score

// **done** check attendance.
// **done** approve attendance.
// get list attendance.
// report attendance of class and student.
// **done** approve request.
// hangfire && send mail

// duplicate -> program
// export file csv && export file csv

// check feedback get evaluate of trainer
// check attendance feature get report

---

- fix all code base

  1. update logging move it to extension **done**
  2. Add ui health app && logging **later**

- Extension
  - cache
  - rate limit - per user
  - health check

- **DONE** jwt, cache move to extension in Infrastructure
    - control what we need to cache no what to cache
-> and register both services is singleton

  - **DONE** remove all apiResult<T> in application

  - choose the type response and catch it web api, controller

  
- refactor source code

  - if must as `{}`
  - avoid user ! operator use is/ is not instead
  - separate code logic u can
  - must do front-end project
    - deploy app

// **done**: pagination don't work syllabus
// **done** data seed move to endpoints 

- crojob service ...
add quartz job
// check middleware timeout && global exception, health check.

// TODO: cleanup source , analyst by rider
//TODO: write test project and test endpoints


// AUTHENTICATE: FORGET PASSWORD. 
// CHECK: attendance, approve request, feedback, score, report, csv

// endpoints: POST: syllabuses/{id}/units
// endpoints: PUT: syllabuses/{id}/units/{unitid}
// endpoints: DELETE: syllabuses/{id}/units/{unitid}

// endpoints: POST: program/{id}/syllabus/{syllabusid}
// endpoints: PUT: program/{id}/syllabus/{syllabusid}
// endpoints: DELETE: program/{id}/syllabus/{syllabusid}

// endpoints: POST: class/{id}/program
// endpoints: PUT: class/{id}/program
// endpoints: DELETE: class/{id}/program
// duplicates

// END