import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Patient } from './patient.model';
@Injectable()
export class PatientFormService {
  public form!: FormGroup;

  public initForm(patient?: Patient): void {
    const form = new FormGroup({
      name: new FormControl(patient?.name || '', [Validators.required]),
      type: new FormControl(patient?.type || '', [Validators.required]),
      ownerSurname: new FormControl(patient?.ownerSurname || '', [Validators.required]),
      birthDate: new FormControl(patient?.birthDate || '', [Validators.required]),
      diagnosis: new FormControl(patient?.diagnosis || '', [Validators.required]),
    });

    this.form = form;
  }

  public prepareData(): Patient {
    const value = this.form.value;

    let data: Patient = {
      name: value.name,
      type: value.type,
      ownerSurname: value.ownerSurname,
      birthDate: value.birthDate,
      diagnosis: value.diagnosis,
    };

    return data;
  }
}

