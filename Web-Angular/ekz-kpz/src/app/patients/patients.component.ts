import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, skipWhile, Subject, switchMap, takeUntil } from 'rxjs';
import { PatientsService } from './patients.service';
import { PatientFormService } from './patient-form.service';
import { Patient } from './patient.model';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { PatientUpsertDialogComponent } from './patient-upsert-dialog/patient-upsert-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit, OnDestroy {
  public patients$!: Observable<Patient[]>;
  public form!: FormGroup;
  private destroy$ = new Subject<void>();

  constructor(private formService: PatientFormService,
    private dialog: MatDialog,
    private patientsService: PatientsService,
    private router: Router){}

  public ngOnInit(): void {
    this.patients$ = this.patientsService.getPatients().pipe(takeUntil(this.destroy$))
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public goToVisits(): void {
    this.router.navigate(['/visits']);
  }

  public openDialog(patient?: Patient): void {
    if (patient === undefined) {
      this.formService.initForm();
    } else {
      this.formService.initForm(patient);
    }
    this.form = this.formService.form;

    const dialogRef = this.dialog.open(PatientUpsertDialogComponent, {
      data: {
        form: this.form,
      },
      autoFocus: false,
    });

    dialogRef
      .afterClosed()
      .pipe(
        takeUntil(this.destroy$),
        skipWhile((result) => result === undefined || !result.isSubmit),
      )
      .subscribe(() => {
        this.upsertPatient(patient?.id);
      });
  }

  private upsertPatient(id?: number): void {
    let data = this.formService.prepareData();

    let upsertedPatient: Patient = {
      id: id,
      name: data.name,
      type: data.type,
      ownerSurname: data.ownerSurname,
      birthDate: data.birthDate,
      diagnosis: data.diagnosis,
    };

    let upsert$: Observable<number>;
    if (id === undefined) {
      upsert$ = this.patientsService.createPatient(upsertedPatient);
    } else {
      upsert$ = this.patientsService.editPatient(upsertedPatient);
    }

    upsert$
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe(() =>{
        this.patients$ = this.patientsService.getPatients().pipe(takeUntil(this.destroy$))
      });
  }

  public deletePatient(id: number): void {
    this.patientsService
      .deletePatient(id)
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe(() => {
        this.patients$ = this.patientsService.getPatients().pipe(takeUntil(this.destroy$))
      });
  }
}
