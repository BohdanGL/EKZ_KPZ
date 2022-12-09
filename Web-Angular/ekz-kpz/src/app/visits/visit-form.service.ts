import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Visit } from './visit.model';
@Injectable()
export class VisitFormService {
  public form!: FormGroup;

  public initForm(visit?: Visit): void {
    const form = new FormGroup({
      date: new FormControl(visit?.date || '', [Validators.required]),
      time: new FormControl(visit?.time || '', [Validators.required]),
      patient: new FormControl(visit?.patient || '', [Validators.required]),
    });

    this.form = form;
  }

  public prepareData(): Visit {
    const value = this.form.value;

    let data: Visit = {
      date: value.date,
      time: value.time,
      patient: value.patient,
    };

    return data;
  }
}

